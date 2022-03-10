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
        [JsonProperty("121984468")]
        public StringField numChildrenRegistered { get; set; }

        [JsonProperty("121984464")]
        public NameField parentGuardianName { get; set; }

        [JsonProperty("121984465")]
        public StringField parentGuardianEmail { get; set; }

        [JsonProperty("121984466")]
        public AddressField address { get; set; }

        [JsonProperty("121984467")]
        public StringField parentGuardianPhone { get; set; }

        [JsonProperty("121984469")]
        public StringField parentGuardianRelationship { get; set; }

        [JsonProperty("121984470")]
        public StringField attendNWB { get; set; }

        [JsonProperty("121984471")]
        public StringField homeChurch { get; set; }

        [JsonProperty("121984472")]
        public StringField invitedBy { get; set; }

        [JsonProperty("121984474")]
        public NameField emergencyContactName { get; set; }

        [JsonProperty("121984475")]
        public StringField emergencyContactPhone { get; set; }

        [JsonProperty("121984476")]
        public StringField emergencyContactRelationship { get; set; }

        [JsonProperty("121984478")]
        public StringField session_c1 { get; set; }

        [JsonProperty("121984479")]
        public NameField name_c1 { get; set; }

        [JsonProperty("121984480")]
        public StringField gender_c1 { get; set; }

        [JsonProperty("121984481")]
        public StringField birthDate_c1 { get; set; }

        [JsonProperty("121984482")]
        public StringField gradeCompleted_c1 { get; set; }

        [JsonProperty("121984483")]
        public StringField hasAllergies_c1 { get; set; }

        [JsonProperty("121984484")]
        public StringField allergiesDescription_c1 { get; set; }

        [JsonProperty("121984485")]
        public StringField hasMedicalConditions_c1 { get; set; }

        [JsonProperty("121984486")]
        public StringField medicalConditionsDescription_c1 { get; set; }

        [JsonProperty("121984487")]
        public StringField hasMedications_c1 { get; set; }

        [JsonProperty("121984488")]
        public StringField medicationsDescription_c1 { get; set; }

        [JsonProperty("121984489")]
        public StringField placeChildWith_c1 { get; set; }


        [JsonProperty("121984491")]
        public StringField session_c2 { get; set; }

        [JsonProperty("121984492")]
        public NameField name_c2 { get; set; }

        [JsonProperty("121984493")]
        public StringField gender_c2 { get; set; }

        [JsonProperty("121984494")]
        public StringField birthDate_c2 { get; set; }

        [JsonProperty("121984495")]
        public StringField gradeCompleted_c2 { get; set; }

        [JsonProperty("121984496")]
        public StringField hasAllergies_c2 { get; set; }

        [JsonProperty("121984497")]
        public StringField allergiesDescription_c2 { get; set; }

        [JsonProperty("121984498")]
        public StringField hasMedicalConditions_c2 { get; set; }

        [JsonProperty("121984499")]
        public StringField medicalConditionsDescription_c2 { get; set; }

        [JsonProperty("121984500")]
        public StringField hasMedications_c2 { get; set; }

        [JsonProperty("121984501")]
        public StringField medicationsDescription_c2 { get; set; }

        [JsonProperty("121984502")]
        public StringField placeChildWith_c2 { get; set; }


        [JsonProperty("121984504")]
        public StringField session_c3 { get; set; }

        [JsonProperty("121984505")]
        public NameField name_c3 { get; set; }

        [JsonProperty("121984506")]
        public StringField gender_c3 { get; set; }

        [JsonProperty("121984507")]
        public StringField birthDate_c3 { get; set; }

        [JsonProperty("121984508")]
        public StringField gradeCompleted_c3 { get; set; }

        [JsonProperty("121984509")]
        public StringField hasAllergies_c3 { get; set; }

        [JsonProperty("121984510")]
        public StringField allergiesDescription_c3 { get; set; }

        [JsonProperty("121984511")]
        public StringField hasMedicalConditions_c3 { get; set; }

        [JsonProperty("121984512")]
        public StringField medicalConditionsDescription_c3 { get; set; }

        [JsonProperty("121984513")]
        public StringField hasMedications_c3 { get; set; }

        [JsonProperty("121984514")]
        public StringField medicationsDescription_c3 { get; set; }

        [JsonProperty("121984515")]
        public StringField placeChildWith_c3 { get; set; }


        [JsonProperty("121984517")]
        public StringField session_c4 { get; set; }

        [JsonProperty("121984518")]
        public NameField name_c4 { get; set; }

        [JsonProperty("121984519")]
        public StringField gender_c4 { get; set; }

        [JsonProperty("121984520")]
        public StringField birthDate_c4 { get; set; }

        [JsonProperty("121984521")]
        public StringField gradeCompleted_c4 { get; set; }

        [JsonProperty("121984522")]
        public StringField hasAllergies_c4 { get; set; }

        [JsonProperty("121984523")]
        public StringField allergiesDescription_c4 { get; set; }

        [JsonProperty("121984524")]
        public StringField hasMedicalConditions_c4 { get; set; }

        [JsonProperty("121984525")]
        public StringField medicalConditionsDescription_c4 { get; set; }

        [JsonProperty("121984526")]
        public StringField hasMedications_c4 { get; set; }

        [JsonProperty("121984527")]
        public StringField medicationsDescription_c4 { get; set; }

        [JsonProperty("121984528")]
        public StringField placeChildWith_c4 { get; set; }


        [JsonProperty("121984530")]
        public StringField session_c5 { get; set; }

        [JsonProperty("121984531")]
        public NameField name_c5 { get; set; }

        [JsonProperty("121984532")]
        public StringField gender_c5 { get; set; }

        [JsonProperty("121984533")]
        public StringField birthDate_c5 { get; set; }

        [JsonProperty("121984534")]
        public StringField gradeCompleted_c5 { get; set; }

        [JsonProperty("121984535")]
        public StringField hasAllergies_c5 { get; set; }

        [JsonProperty("121984536")]
        public StringField allergiesDescription_c5 { get; set; }

        [JsonProperty("121984537")]
        public StringField hasMedicalConditions_c5 { get; set; }

        [JsonProperty("121984538")]
        public StringField medicalConditionsDescription_c5 { get; set; }

        [JsonProperty("121984539")]
        public StringField hasMedications_c5 { get; set; }

        [JsonProperty("121984540")]
        public StringField medicationsDescription_c5 { get; set; }

        [JsonProperty("121984541")]
        public StringField placeChildWith_c5 { get; set; }

        [JsonProperty("121984543")]
        public CheckboxField consent { get; set; }

        [JsonProperty("121984544")]
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
