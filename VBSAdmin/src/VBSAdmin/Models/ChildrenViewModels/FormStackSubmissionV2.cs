using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace VBSAdmin.Models.ChildrenViewModels
{
    public class FormStackSubmissionV2
    {
        public int total { get; set; }
        public int pages { get; set; }

        public List<SubmissionV2> submissions { get; set; }

    }

    public class SubmissionV2
    {
        public int id { get; set; }
        public string timestamp { get; set; }

        public DataV2 data { get; set; }

    }


    public class DataV2
    {
        [JsonProperty("142602625")]
        public StringField numChildrenRegistered { get; set; }

        [JsonProperty("142602621")]
        public NameField parentGuardianName { get; set; }

        [JsonProperty("142602622")]
        public StringField parentGuardianEmail { get; set; }

        [JsonProperty("142602623")]
        public AddressField address { get; set; }

        [JsonProperty("142602624")]
        public StringField parentGuardianPhone { get; set; }

        [JsonProperty("142602626")]
        public StringField parentGuardianRelationship { get; set; }

        [JsonProperty("142602627")]
        public StringField attendNWB { get; set; }

        [JsonProperty("142602628")]
        public StringField homeChurch { get; set; }

        [JsonProperty("142602629")]
        public StringField invitedBy { get; set; }

        [JsonProperty("142602631")]
        public NameField emergencyContactName { get; set; }

        [JsonProperty("142602632")]
        public StringField emergencyContactPhone { get; set; }

        [JsonProperty("142602633")]
        public StringField emergencyContactRelationship { get; set; }

        [JsonProperty("142602635")]
        public StringField session_c1 { get; set; }

        [JsonProperty("142602636")]
        public NameField name_c1 { get; set; }

        [JsonProperty("142602637")]
        public StringField gender_c1 { get; set; }

        [JsonProperty("142602639")]
        public StringField birthDate_c1 { get; set; }

        [JsonProperty("142602642")]
        public StringField gradeCompleted_c1 { get; set; }

        [JsonProperty("142602643")]
        public StringField hasAllergies_c1 { get; set; }

        [JsonProperty("142602644")]
        public StringField allergiesDescription_c1 { get; set; }

        [JsonProperty("142602645")]
        public StringField hasMedicalConditions_c1 { get; set; }

        [JsonProperty("142602646")]
        public StringField medicalConditionsDescription_c1 { get; set; }

        [JsonProperty("142602647")]
        public StringField hasMedications_c1 { get; set; }

        [JsonProperty("142602648")]
        public StringField medicationsDescription_c1 { get; set; }

        [JsonProperty("142602649")]
        public StringField placeChildWith_c1 { get; set; }


        [JsonProperty("142602651")]
        public StringField session_c2 { get; set; }

        [JsonProperty("142602652")]
        public NameField name_c2 { get; set; }

        [JsonProperty("142602653")]
        public StringField gender_c2 { get; set; }

        [JsonProperty("142602654")]
        public StringField birthDate_c2 { get; set; }

        [JsonProperty("142602657")]
        public StringField gradeCompleted_c2 { get; set; }

        [JsonProperty("142602658")]
        public StringField hasAllergies_c2 { get; set; }

        [JsonProperty("142602659")]
        public StringField allergiesDescription_c2 { get; set; }

        [JsonProperty("142602660")]
        public StringField hasMedicalConditions_c2 { get; set; }

        [JsonProperty("142602661")]
        public StringField medicalConditionsDescription_c2 { get; set; }

        [JsonProperty("142602662")]
        public StringField hasMedications_c2 { get; set; }

        [JsonProperty("142602663")]
        public StringField medicationsDescription_c2 { get; set; }

        [JsonProperty("142602664")]
        public StringField placeChildWith_c2 { get; set; }


        [JsonProperty("142602666")]
        public StringField session_c3 { get; set; }

        [JsonProperty("142602667")]
        public NameField name_c3 { get; set; }

        [JsonProperty("142602668")]
        public StringField gender_c3 { get; set; }

        [JsonProperty("142602669")]
        public StringField birthDate_c3 { get; set; }

        [JsonProperty("142602672")]
        public StringField gradeCompleted_c3 { get; set; }

        [JsonProperty("142602673")]
        public StringField hasAllergies_c3 { get; set; }

        [JsonProperty("142602674")]
        public StringField allergiesDescription_c3 { get; set; }

        [JsonProperty("142602675")]
        public StringField hasMedicalConditions_c3 { get; set; }

        [JsonProperty("142602676")]
        public StringField medicalConditionsDescription_c3 { get; set; }

        [JsonProperty("142602677")]
        public StringField hasMedications_c3 { get; set; }

        [JsonProperty("142602678")]
        public StringField medicationsDescription_c3 { get; set; }

        [JsonProperty("142602679")]
        public StringField placeChildWith_c3 { get; set; }


        [JsonProperty("142602681")]
        public StringField session_c4 { get; set; }

        [JsonProperty("142602682")]
        public NameField name_c4 { get; set; }

        [JsonProperty("142602683")]
        public StringField gender_c4 { get; set; }

        [JsonProperty("142602684")]
        public StringField birthDate_c4 { get; set; }

        [JsonProperty("142602687")]
        public StringField gradeCompleted_c4 { get; set; }

        [JsonProperty("142602688")]
        public StringField hasAllergies_c4 { get; set; }

        [JsonProperty("142602689")]
        public StringField allergiesDescription_c4 { get; set; }

        [JsonProperty("142602690")]
        public StringField hasMedicalConditions_c4 { get; set; }

        [JsonProperty("142602691")]
        public StringField medicalConditionsDescription_c4 { get; set; }

        [JsonProperty("142602692")]
        public StringField hasMedications_c4 { get; set; }

        [JsonProperty("142602693")]
        public StringField medicationsDescription_c4 { get; set; }

        [JsonProperty("142602694")]
        public StringField placeChildWith_c4 { get; set; }


        [JsonProperty("142602696")]
        public StringField session_c5 { get; set; }

        [JsonProperty("142602697")]
        public NameField name_c5 { get; set; }

        [JsonProperty("142602698")]
        public StringField gender_c5 { get; set; }

        [JsonProperty("142602699")]
        public StringField birthDate_c5 { get; set; }

        [JsonProperty("142602702")]
        public StringField gradeCompleted_c5 { get; set; }

        [JsonProperty("142602703")]
        public StringField hasAllergies_c5 { get; set; }

        [JsonProperty("142602704")]
        public StringField allergiesDescription_c5 { get; set; }

        [JsonProperty("142602705")]
        public StringField hasMedicalConditions_c5 { get; set; }

        [JsonProperty("142602706")]
        public StringField medicalConditionsDescription_c5 { get; set; }

        [JsonProperty("142602707")]
        public StringField hasMedications_c5 { get; set; }

        [JsonProperty("142602708")]
        public StringField medicationsDescription_c5 { get; set; }

        [JsonProperty("142602709")]
        public StringField placeChildWith_c5 { get; set; }

        [JsonProperty("142602711")]
        public CheckboxField consent { get; set; }

        [JsonProperty("142602712")]
        public StringField signatureImageUrl { get; set; }

    }

    public class CheckboxField
    {
        public string field { get; set; }
        public string flat_value { get; set; }
    }

    public class AddressField
    {
        public string field { get; set; }
        public AddressValue value { get; set; }
    }

    public class AddressValue
    {
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
    }

    public class StringField
    {
        public string field { get; set; }
        public string value { get; set; }
    }

    public class NameField
    {
        public string field { get; set; }
        public NameValue value { get; set; }
    }

    public class NameValue
    {
        public string first { get; set; }
        public string last { get; set; }
    }


}
