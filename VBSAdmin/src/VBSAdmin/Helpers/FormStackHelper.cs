using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using VBSAdmin.Data;
using VBSAdmin.Data.VBSAdminModels;
using VBSAdmin.Models.ChildrenViewModels;
using Newtonsoft.Json;


namespace VBSAdmin.Helpers
{
    public class FormStackHelper
    {
        public string BaseAddress { get; set; }
        public string ApiKey { get; set; }
        public string ResponseType { get; set; }
        public int VbsId { get; set; }
        public int AMSessionId { get; set; }
        public int PMSessionId { get; set; }

        private ApplicationDbContext _context;

        private VBS vBS { get; set; }


        public FormStackHelper(string baseAddress, string apiKey, string responseType, VBS vbs, int amSessionId, int pmSessionId, ApplicationDbContext dbContext)
        {
            BaseAddress = baseAddress;
            ApiKey = apiKey;
            ResponseType = responseType;
            vBS = vbs;
            VbsId = vbs.Id;
            AMSessionId = amSessionId;
            PMSessionId = pmSessionId;
            _context = dbContext;
        }

        public async Task PopulateLatestSubmissions(string formId, string minTime)
        {
            FormStackSubmission submissions = await GetSubmissionPage(1, formId, minTime);
            if (submissions != null && submissions.response.submissions.Count > 0)
            {
                int numPages = submissions.response.pages;

                string latestTimestamp = "";
                foreach (Submission submission in submissions.response.submissions)
                {
                    await ImportSubmission(submission);
                    latestTimestamp = submission.timestamp;
                }


                if (numPages > 1)
                {
                    for (int i = 2; i <= numPages; i++)
                    {
                        submissions = await GetSubmissionPage(i, formId, minTime);
                        if (submissions != null && submissions.response.submissions.Count > 0)
                        {
                            foreach (Submission submission in submissions.response.submissions)
                            {
                                await ImportSubmission(submission);
                                latestTimestamp = submission.timestamp;
                            }
                        }
                    }
                }

                //Save the latest timestamp to the database
                vBS.FormStackLastImportDateTime = Convert.ToDateTime(latestTimestamp).AddSeconds(1);
                await _context.SaveChangesAsync();
                return;
            }
            return;
        }

        private async Task<FormStackSubmission> GetSubmissionPage(int page, string formId, string minTime)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(this.BaseAddress);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string path = "data?api_key=" + ApiKey + "&type=" + ResponseType + "&id=" + formId + "&per_page=100&page=" + page;
            if (!string.IsNullOrEmpty(minTime))
            {
                path = path + "&min_time=" + minTime;
            }

            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                FormStackSubmission submissionResponse;
                string jsonString = response.Content.ReadAsStringAsync().Result;
                submissionResponse = JsonConvert.DeserializeObject<FormStackSubmission>(jsonString);

                return submissionResponse;
            }

            return null;
        }

        private async Task ImportSubmission(Submission submission)
        {
            FormStackForm form = new FormStackForm();
            try
            {

                foreach (VBSAdmin.Models.ChildrenViewModels.Item data in submission.data)
                {
                    switch (Convert.ToInt32(data.field))
                    {
                        case (int)FormStackForm2018Enum.number_of_your_children_being_registered_now:
                            form.number_of_your_children_being_registered_now = Convert.ToInt32(data.value);
                            break;
                        case (int)FormStackForm2018Enum.parentguardian_name:
                            var firstSpaceIndex = data.value.IndexOf(" ");
                            form.parentguardian_first_name = data.value.Substring(0, firstSpaceIndex);
                            form.parentguardian_last_name = data.value.Substring(firstSpaceIndex + 1);
                            break;
                        case (int)FormStackForm2018Enum.email:
                            form.email = data.value.Trim();
                            break;
                        case (int)FormStackForm2018Enum.address:
                            string addr, city, state, zip;
                            int len = data.value.Length;
                            zip = data.value.Substring(len - 5, 5).Trim();
                            state = data.value.Substring(len - 8, 2).Trim();
                            var temp = data.value.Substring(0, len - 10);
                            var tokens = temp.Split('\n');
                            addr = tokens[0].Trim();
                            city = tokens[tokens.Length - 1].Trim();

                            form.address = addr;
                            form.city = city;
                            form.state = state;
                            form.zip = zip;
                            break;
                        case (int)FormStackForm2018Enum.parentguardian_phone:
                            var phone = data.value.Trim();
                            phone = phone.Replace(" ", "");
                            phone = phone.Replace("(", "");
                            phone = phone.Replace(")", "");
                            phone = phone.Replace("-", "");
                            form.parentguardian_phone = phone;
                            break;
                        case (int)FormStackForm2018Enum.relationship_to_children_being_registered:
                            form.relationship_to_children_being_registered = data.value.Trim();
                            break;
                        case (int)FormStackForm2018Enum.does_your_family_attend_northwest_bible_church:
                            form.does_your_family_attend_northwest_bible_church = data.value.Trim();
                            break;
                        case (int)FormStackForm2018Enum.what_church_does_your_family_call_home:
                            form.what_church_does_your_family_call_home = data.value.Trim();
                            break;
                        case (int)FormStackForm2018Enum.who_invited_you_to_vbs_at_northwest:
                            form.who_invited_you_to_vbs_at_northwest = data.value.Trim();
                            break;
                        case (int)FormStackForm2018Enum.emergency_contact_name:
                            var emfirstSpaceIndex = data.value.IndexOf(" ");
                            form.emergency_contact_first_name = data.value.Substring(0, emfirstSpaceIndex).Trim();
                            form.emergency_contact_last_name = data.value.Substring(emfirstSpaceIndex + 1).Trim();
                            break;
                        case (int)FormStackForm2018Enum.emergency_contact_phone:
                            var emphone = data.value.Trim();
                            emphone = emphone.Replace(" ", "");
                            emphone = emphone.Replace("(", "");
                            emphone = emphone.Replace(")", "");
                            emphone = emphone.Replace("-", "");
                            form.emergency_contact_phone = emphone;
                            break;
                        case (int)FormStackForm2018Enum.emergency_contact_relationship:
                            form.emergency_contact_relationship = data.value.Trim();
                            break;
                        case (int)FormStackForm2018Enum.morning_or_evening_session_c1:
                            form.morning_or_evening_session_c1 = data.value.Trim();
                            break;
                        case (int)FormStackForm2018Enum.name_c1:
                            var firstSpaceIndex_c1 = data.value.IndexOf(" ");
                            form.first_name_c1 = data.value.Substring(0, firstSpaceIndex_c1).Trim();
                            form.last_name_c1 = data.value.Substring(firstSpaceIndex_c1 + 1).Trim();
                            break;
                        case (int)FormStackForm2018Enum.gender_c1:
                            form.gender_c1 = data.value.Trim();
                            break;
                        case (int)FormStackForm2018Enum.birth_date_c1:
                            form.birth_date_c1 = data.value.Trim();
                            break;
                        case (int)FormStackForm2018Enum.grade_completed_c1:
                            form.grade_completed_c1 = data.value.Trim();
                            break;
                        case (int)FormStackForm2018Enum.does_this_child_have_any_allergies_c1:
                            form.does_this_child_have_any_allergies_c1 = data.value.Trim();
                            break;
                        case (int)FormStackForm2018Enum.allergies_please_explain_c1:
                            form.allergies_please_explain_c1 = data.value.Trim();
                            break;
                        case (int)FormStackForm2018Enum.does_this_child_have_any_medical_conditions_c1:
                            form.does_this_child_have_any_medical_conditions_c1 = data.value.Trim();
                            break;
                        case (int)FormStackForm2018Enum.medical_conditions_please_explain_c1:
                            form.medical_conditions_please_explain_c1 = data.value.Trim();
                            break;
                        case (int)FormStackForm2018Enum.is_this_child_taking_any_medications_c1:
                            form.is_this_child_taking_any_medications_c1 = data.value.Trim();
                            break;
                        case (int)FormStackForm2018Enum.medications_please_explain_c1:
                            form.medications_please_explain_c1 = data.value.Trim();
                            break;
                        case (int)FormStackForm2018Enum.please_place_my_child_with_c1:
                            form.please_place_my_child_with_c1 = data.value.Trim();
                            break;
                        case (int)FormStackForm2018Enum.morning_or_evening_session_c2:
                            form.morning_or_evening_session_c2 = data.value.Trim();
                            break;
                        case (int)FormStackForm2018Enum.name_c2:
                            var firstSpaceIndex_c2 = data.value.IndexOf(" ");
                            form.first_name_c2 = data.value.Substring(0, firstSpaceIndex_c2).Trim();
                            form.last_name_c2 = data.value.Substring(firstSpaceIndex_c2 + 1).Trim();
                            break;
                        case (int)FormStackForm2018Enum.gender_c2:
                            form.gender_c2 = data.value.Trim();
                            break;
                        case (int)FormStackForm2018Enum.birth_date_c2:
                            form.birth_date_c2 = data.value.Trim();
                            break;
                        case (int)FormStackForm2018Enum.grade_completed_c2:
                            form.grade_completed_c2 = data.value.Trim();
                            break;
                        case (int)FormStackForm2018Enum.does_this_child_have_any_allergies_c2:
                            form.does_this_child_have_any_allergies_c2 = data.value.Trim();
                            break;
                        case (int)FormStackForm2018Enum.allergies_please_explain_c2:
                            form.allergies_please_explain_c2 = data.value.Trim();
                            break;
                        case (int)FormStackForm2018Enum.does_this_child_have_any_medical_conditions_c2:
                            form.does_this_child_have_any_medical_conditions_c2 = data.value.Trim();
                            break;
                        case (int)FormStackForm2018Enum.medical_conditions_please_explain_c2:
                            form.medical_conditions_please_explain_c2 = data.value.Trim();
                            break;
                        case (int)FormStackForm2018Enum.is_this_child_taking_any_medications_c2:
                            form.is_this_child_taking_any_medications_c2 = data.value.Trim();
                            break;
                        case (int)FormStackForm2018Enum.medications_please_explain_c2:
                            form.medications_please_explain_c2 = data.value.Trim();
                            break;
                        case (int)FormStackForm2018Enum.please_place_my_child_with_c2:
                            form.please_place_my_child_with_c2 = data.value.Trim();
                            break;
                        case (int)FormStackForm2018Enum.morning_or_evening_session_c3:
                            form.morning_or_evening_session_c3 = data.value.Trim();
                            break;
                        case (int)FormStackForm2018Enum.name_c3:
                            var firstSpaceIndex_c3 = data.value.IndexOf(" ");
                            form.first_name_c3 = data.value.Substring(0, firstSpaceIndex_c3).Trim();
                            form.last_name_c3 = data.value.Substring(firstSpaceIndex_c3 + 1).Trim();
                            break;
                        case (int)FormStackForm2018Enum.gender_c3:
                            form.gender_c3 = data.value.Trim();
                            break;
                        case (int)FormStackForm2018Enum.birth_date_c3:
                            form.birth_date_c3 = data.value.Trim();
                            break;
                        case (int)FormStackForm2018Enum.grade_completed_c3:
                            form.grade_completed_c3 = data.value.Trim();
                            break;
                        case (int)FormStackForm2018Enum.does_this_child_have_any_allergies_c3:
                            form.does_this_child_have_any_allergies_c3 = data.value.Trim();
                            break;
                        case (int)FormStackForm2018Enum.allergies_please_explain_c3:
                            form.allergies_please_explain_c3 = data.value.Trim();
                            break;
                        case (int)FormStackForm2018Enum.does_this_child_have_any_medical_conditions_c3:
                            form.does_this_child_have_any_medical_conditions_c3 = data.value.Trim();
                            break;
                        case (int)FormStackForm2018Enum.medical_conditions_please_explain_c3:
                            form.medical_conditions_please_explain_c3 = data.value.Trim();
                            break;
                        case (int)FormStackForm2018Enum.is_this_child_taking_any_medications_c3:
                            form.is_this_child_taking_any_medications_c3 = data.value.Trim();
                            break;
                        case (int)FormStackForm2018Enum.medications_please_explain_c3:
                            form.medications_please_explain_c3 = data.value.Trim();
                            break;
                        case (int)FormStackForm2018Enum.please_place_my_child_with_c3:
                            form.please_place_my_child_with_c3 = data.value.Trim();
                            break;
                        case (int)FormStackForm2018Enum.morning_or_evening_session_c4:
                            form.morning_or_evening_session_c4 = data.value.Trim();
                            break;
                        case (int)FormStackForm2018Enum.name_c4:
                            var firstSpaceIndex_c4 = data.value.IndexOf(" ");
                            form.first_name_c4 = data.value.Substring(0, firstSpaceIndex_c4).Trim();
                            form.last_name_c4 = data.value.Substring(firstSpaceIndex_c4 + 1).Trim();
                            break;
                        case (int)FormStackForm2018Enum.gender_c4:
                            form.gender_c4 = data.value.Trim();
                            break;
                        case (int)FormStackForm2018Enum.birth_date_c4:
                            form.birth_date_c4 = data.value.Trim();
                            break;
                        case (int)FormStackForm2018Enum.grade_completed_c4:
                            form.grade_completed_c4 = data.value.Trim();
                            break;
                        case (int)FormStackForm2018Enum.does_this_child_have_any_allergies_c4:
                            form.does_this_child_have_any_allergies_c4 = data.value.Trim();
                            break;
                        case (int)FormStackForm2018Enum.allergies_please_explain_c4:
                            form.allergies_please_explain_c4 = data.value.Trim();
                            break;
                        case (int)FormStackForm2018Enum.does_this_child_have_any_medical_conditions_c4:
                            form.does_this_child_have_any_medical_conditions_c4 = data.value.Trim();
                            break;
                        case (int)FormStackForm2018Enum.medical_conditions_please_explain_c4:
                            form.medical_conditions_please_explain_c4 = data.value.Trim();
                            break;
                        case (int)FormStackForm2018Enum.is_this_child_taking_any_medications_c4:
                            form.is_this_child_taking_any_medications_c4 = data.value.Trim();
                            break;
                        case (int)FormStackForm2018Enum.medications_please_explain_c4:
                            form.medications_please_explain_c4 = data.value.Trim();
                            break;
                        case (int)FormStackForm2018Enum.please_place_my_child_with_c4:
                            form.please_place_my_child_with_c4 = data.value.Trim();
                            break;
                        case (int)FormStackForm2018Enum.morning_or_evening_session_c5:
                            form.morning_or_evening_session_c5 = data.value.Trim();
                            break;
                        case (int)FormStackForm2018Enum.name_c5:
                            var firstSpaceIndex_c5 = data.value.IndexOf(" ");
                            form.first_name_c5 = data.value.Substring(0, firstSpaceIndex_c5).Trim();
                            form.last_name_c5 = data.value.Substring(firstSpaceIndex_c5 + 1).Trim();
                            break;
                        case (int)FormStackForm2018Enum.gender_c5:
                            form.gender_c5 = data.value.Trim();
                            break;
                        case (int)FormStackForm2018Enum.birth_date_c5:
                            form.birth_date_c5 = data.value.Trim();
                            break;
                        case (int)FormStackForm2018Enum.grade_completed_c5:
                            form.grade_completed_c5 = data.value.Trim();
                            break;
                        case (int)FormStackForm2018Enum.does_this_child_have_any_allergies_c5:
                            form.does_this_child_have_any_allergies_c5 = data.value.Trim();
                            break;
                        case (int)FormStackForm2018Enum.allergies_please_explain_c5:
                            form.allergies_please_explain_c5 = data.value.Trim();
                            break;
                        case (int)FormStackForm2018Enum.does_this_child_have_any_medical_conditions_c5:
                            form.does_this_child_have_any_medical_conditions_c5 = data.value.Trim();
                            break;
                        case (int)FormStackForm2018Enum.medical_conditions_please_explain_c5:
                            form.medical_conditions_please_explain_c5 = data.value.Trim();
                            break;
                        case (int)FormStackForm2018Enum.is_this_child_taking_any_medications_c5:
                            form.is_this_child_taking_any_medications_c5 = data.value.Trim();
                            break;
                        case (int)FormStackForm2018Enum.medications_please_explain_c5:
                            form.medications_please_explain_c5 = data.value.Trim();
                            break;
                        case (int)FormStackForm2018Enum.please_place_my_child_with_c5:
                            form.please_place_my_child_with_c5 = data.value.Trim();
                            break;
                        default:
                            break;
                    }
                }
            }
            catch(Exception e)
            {
                var message = e.Message;
            }

            for(int i = 1; i<=form.number_of_your_children_being_registered_now; i++)
            {
                Child child = new Child
                {
                    VBSId = VbsId,
                    DateOfRegistration = Convert.ToDateTime(submission.timestamp),
                    Address1 = form.address,
                    City = form.city,
                    State = form.state,
                    Zip = form.zip,
                    AttendHostChurch = (form.does_your_family_attend_northwest_bible_church.ToLower() == "no") ? false : true,
                    GuardianFirstName = form.parentguardian_first_name,
                    GuardianLastName = form.parentguardian_last_name,
                    GuardianPhone = form.parentguardian_phone,
                    GuardianEmail = form.email,
                    GuardianChildRelationship = form.relationship_to_children_being_registered,
                    EmergencyContactFirstName = form.emergency_contact_first_name,
                    EmergencyContactLastName = form.emergency_contact_last_name,
                    EmergencyContactChildRelationship = form.emergency_contact_relationship,
                    EmergencyContactPhone = form.emergency_contact_phone,
                    HomeChurch = form.what_church_does_your_family_call_home,
                    InvitedBy = form.who_invited_you_to_vbs_at_northwest,
                    ClassroomId = null
                };

                if(i == 1)
                {
                    child.FirstName = form.first_name_c1;
                    child.LastName = form.last_name_c1;
                    child.AllergiesDescription = form.allergies_please_explain_c1;
                    child.HasAllergies = (form.does_this_child_have_any_allergies_c1.ToLower() == "yes") ? true : false;
                    child.HasMedicalCondition = (form.does_this_child_have_any_medical_conditions_c1.ToLower() == "yes") ? true : false;
                    child.MedicalConditionDescription = form.medical_conditions_please_explain_c1;
                    child.HasMedications = (form.is_this_child_taking_any_medications_c1.ToLower() == "yes") ? true : false;
                    child.MedicationDescription = form.medications_please_explain_c1;
                    child.DateOfBirth = Convert.ToDateTime(form.birth_date_c1);
                    child.Gender = (form.gender_c1.ToLower() == "male") ? Enums.ChildGender.Boy : Enums.ChildGender.Girl;
                    child.SessionId = (form.morning_or_evening_session_c1.ToLower().Contains("morning")) ? AMSessionId : PMSessionId;
                    child.PlaceChildWithRequest = form.please_place_my_child_with_c1;
                    switch (form.grade_completed_c1)
                    {
                        case "16":
                            child.GradeCompleted = Enums.ClassGrade.PreSchool;
                            break;
                        case "17":
                            child.GradeCompleted = Enums.ClassGrade.PreK;
                            break;
                        case "13":
                            child.GradeCompleted = Enums.ClassGrade.Kindergarten;
                            break;
                        case "1":
                            child.GradeCompleted = Enums.ClassGrade.First;
                            break;
                        case "2":
                            child.GradeCompleted = Enums.ClassGrade.Second;
                            break;
                        case "3":
                            child.GradeCompleted = Enums.ClassGrade.Third;
                            break;
                        case "4":
                            child.GradeCompleted = Enums.ClassGrade.Fourth;
                            break;
                        case "5":
                            child.GradeCompleted = Enums.ClassGrade.Fifth;
                            break;
                        case "6":
                            child.GradeCompleted = Enums.ClassGrade.Sixth;
                            break;
                        default:
                            break;
                    }
                }

                if (i == 2)
                {
                    child.FirstName = form.first_name_c2;
                    child.LastName = form.last_name_c2;
                    child.AllergiesDescription = form.allergies_please_explain_c2;
                    child.HasAllergies = (form.does_this_child_have_any_allergies_c2.ToLower() == "yes") ? true : false;
                    child.HasMedicalCondition = (form.does_this_child_have_any_medical_conditions_c2.ToLower() == "yes") ? true : false;
                    child.MedicalConditionDescription = form.medical_conditions_please_explain_c2;
                    child.HasMedications = (form.is_this_child_taking_any_medications_c2.ToLower() == "yes") ? true : false;
                    child.MedicationDescription = form.medications_please_explain_c2;
                    child.DateOfBirth = Convert.ToDateTime(form.birth_date_c2);
                    child.Gender = (form.gender_c2.ToLower() == "male") ? Enums.ChildGender.Boy : Enums.ChildGender.Girl;
                    child.SessionId = (form.morning_or_evening_session_c2.ToLower().Contains("morning")) ? AMSessionId : PMSessionId;
                    child.PlaceChildWithRequest = form.please_place_my_child_with_c2;
                    switch (form.grade_completed_c2)
                    {
                        case "16":
                            child.GradeCompleted = Enums.ClassGrade.PreSchool;
                            break;
                        case "17":
                            child.GradeCompleted = Enums.ClassGrade.PreK;
                            break;
                        case "13":
                            child.GradeCompleted = Enums.ClassGrade.Kindergarten;
                            break;
                        case "1":
                            child.GradeCompleted = Enums.ClassGrade.First;
                            break;
                        case "2":
                            child.GradeCompleted = Enums.ClassGrade.Second;
                            break;
                        case "3":
                            child.GradeCompleted = Enums.ClassGrade.Third;
                            break;
                        case "4":
                            child.GradeCompleted = Enums.ClassGrade.Fourth;
                            break;
                        case "5":
                            child.GradeCompleted = Enums.ClassGrade.Fifth;
                            break;
                        case "6":
                            child.GradeCompleted = Enums.ClassGrade.Sixth;
                            break;
                        default:
                            break;
                    }
                }

                if (i == 3)
                {
                    child.FirstName = form.first_name_c3;
                    child.LastName = form.last_name_c3;
                    child.AllergiesDescription = form.allergies_please_explain_c3;
                    child.HasAllergies = (form.does_this_child_have_any_allergies_c3.ToLower() == "yes") ? true : false;
                    child.HasMedicalCondition = (form.does_this_child_have_any_medical_conditions_c3.ToLower() == "yes") ? true : false;
                    child.MedicalConditionDescription = form.medical_conditions_please_explain_c3;
                    child.HasMedications = (form.is_this_child_taking_any_medications_c3.ToLower() == "yes") ? true : false;
                    child.MedicationDescription = form.medications_please_explain_c3;
                    child.DateOfBirth = Convert.ToDateTime(form.birth_date_c3);
                    child.Gender = (form.gender_c3.ToLower() == "male") ? Enums.ChildGender.Boy : Enums.ChildGender.Girl;
                    child.SessionId = (form.morning_or_evening_session_c3.ToLower().Contains("morning")) ? AMSessionId : PMSessionId;
                    child.PlaceChildWithRequest = form.please_place_my_child_with_c3;
                    switch (form.grade_completed_c3)
                    {
                        case "16":
                            child.GradeCompleted = Enums.ClassGrade.PreSchool;
                            break;
                        case "17":
                            child.GradeCompleted = Enums.ClassGrade.PreK;
                            break;
                        case "13":
                            child.GradeCompleted = Enums.ClassGrade.Kindergarten;
                            break;
                        case "1":
                            child.GradeCompleted = Enums.ClassGrade.First;
                            break;
                        case "2":
                            child.GradeCompleted = Enums.ClassGrade.Second;
                            break;
                        case "3":
                            child.GradeCompleted = Enums.ClassGrade.Third;
                            break;
                        case "4":
                            child.GradeCompleted = Enums.ClassGrade.Fourth;
                            break;
                        case "5":
                            child.GradeCompleted = Enums.ClassGrade.Fifth;
                            break;
                        case "6":
                            child.GradeCompleted = Enums.ClassGrade.Sixth;
                            break;
                        default:
                            break;
                    }
                }

                if (i == 4)
                {
                    child.FirstName = form.first_name_c4;
                    child.LastName = form.last_name_c4;
                    child.AllergiesDescription = form.allergies_please_explain_c4;
                    child.HasAllergies = (form.does_this_child_have_any_allergies_c4.ToLower() == "yes") ? true : false;
                    child.HasMedicalCondition = (form.does_this_child_have_any_medical_conditions_c4.ToLower() == "yes") ? true : false;
                    child.MedicalConditionDescription = form.medical_conditions_please_explain_c4;
                    child.HasMedications = (form.is_this_child_taking_any_medications_c4.ToLower() == "yes") ? true : false;
                    child.MedicationDescription = form.medications_please_explain_c4;
                    child.DateOfBirth = Convert.ToDateTime(form.birth_date_c4);
                    child.Gender = (form.gender_c4.ToLower() == "male") ? Enums.ChildGender.Boy : Enums.ChildGender.Girl;
                    child.SessionId = (form.morning_or_evening_session_c4.ToLower().Contains("morning")) ? AMSessionId : PMSessionId;
                    child.PlaceChildWithRequest = form.please_place_my_child_with_c4;
                    switch (form.grade_completed_c4)
                    {
                        case "16":
                            child.GradeCompleted = Enums.ClassGrade.PreSchool;
                            break;
                        case "17":
                            child.GradeCompleted = Enums.ClassGrade.PreK;
                            break;
                        case "13":
                            child.GradeCompleted = Enums.ClassGrade.Kindergarten;
                            break;
                        case "1":
                            child.GradeCompleted = Enums.ClassGrade.First;
                            break;
                        case "2":
                            child.GradeCompleted = Enums.ClassGrade.Second;
                            break;
                        case "3":
                            child.GradeCompleted = Enums.ClassGrade.Third;
                            break;
                        case "4":
                            child.GradeCompleted = Enums.ClassGrade.Fourth;
                            break;
                        case "5":
                            child.GradeCompleted = Enums.ClassGrade.Fifth;
                            break;
                        case "6":
                            child.GradeCompleted = Enums.ClassGrade.Sixth;
                            break;
                        default:
                            break;
                    }
                }

                if (i == 5)
                {
                    child.FirstName = form.first_name_c5;
                    child.LastName = form.last_name_c5;
                    child.AllergiesDescription = form.allergies_please_explain_c5;
                    child.HasAllergies = (form.does_this_child_have_any_allergies_c5.ToLower() == "yes") ? true : false;
                    child.HasMedicalCondition = (form.does_this_child_have_any_medical_conditions_c5.ToLower() == "yes") ? true : false;
                    child.MedicalConditionDescription = form.medical_conditions_please_explain_c5;
                    child.HasMedications = (form.is_this_child_taking_any_medications_c5.ToLower() == "yes") ? true : false;
                    child.MedicationDescription = form.medications_please_explain_c5;
                    child.DateOfBirth = Convert.ToDateTime(form.birth_date_c5);
                    child.Gender = (form.gender_c5.ToLower() == "male") ? Enums.ChildGender.Boy : Enums.ChildGender.Girl;
                    child.SessionId = (form.morning_or_evening_session_c5.ToLower().Contains("morning")) ? AMSessionId : PMSessionId;
                    child.PlaceChildWithRequest = form.please_place_my_child_with_c5;
                    switch (form.grade_completed_c5)
                    {
                        case "16":
                            child.GradeCompleted = Enums.ClassGrade.PreSchool;
                            break;
                        case "17":
                            child.GradeCompleted = Enums.ClassGrade.PreK;
                            break;
                        case "13":
                            child.GradeCompleted = Enums.ClassGrade.Kindergarten;
                            break;
                        case "1":
                            child.GradeCompleted = Enums.ClassGrade.First;
                            break;
                        case "2":
                            child.GradeCompleted = Enums.ClassGrade.Second;
                            break;
                        case "3":
                            child.GradeCompleted = Enums.ClassGrade.Third;
                            break;
                        case "4":
                            child.GradeCompleted = Enums.ClassGrade.Fourth;
                            break;
                        case "5":
                            child.GradeCompleted = Enums.ClassGrade.Fifth;
                            break;
                        case "6":
                            child.GradeCompleted = Enums.ClassGrade.Sixth;
                            break;
                        default:
                            break;
                    }
                }

                //Get the geocode for the registered child
                GetGeoCodeResponse geoResponse = await GeocodeHelper.GetGeoCode(child);
                if (geoResponse != null)
                {
                    child.Latitude = geoResponse.Lat;
                    child.Longitude = geoResponse.Long;
                }


                //Add the child to the db context
                _context.Add(child);
            }

            //Save the new children to the db
            await _context.SaveChangesAsync();

            return;
        }
    }
}
