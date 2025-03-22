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
        [JsonProperty("162000391")]
        public StringField numChildrenRegistered { get; set; }

        [JsonProperty("162000387")]
        public NameField parentGuardianName { get; set; }

        [JsonProperty("162000388")]
        public StringField parentGuardianEmail { get; set; }

        [JsonProperty("162000389")]
        public AddressField address { get; set; }

        [JsonProperty("162000390")]
        public StringField parentGuardianPhone { get; set; }

        [JsonProperty("162000392")]
        public StringField parentGuardianRelationship { get; set; }

        [JsonProperty("162000393")]
        public StringField attendNWB { get; set; }

        [JsonProperty("162000394")]
        public StringField homeChurch { get; set; }

        [JsonProperty("162000395")]
        public StringField invitedBy { get; set; }

        [JsonProperty("162000397")]
        public NameField emergencyContactName { get; set; }

        [JsonProperty("162000398")]
        public StringField emergencyContactPhone { get; set; }

        [JsonProperty("162000399")]
        public StringField emergencyContactRelationship { get; set; }

        [JsonProperty("162000401")]
        public StringField session_c1 { get; set; }

        [JsonProperty("162000402")]
        public NameField name_c1 { get; set; }

        [JsonProperty("162000403")]
        public StringField gender_c1 { get; set; }

        [JsonProperty("162000405")]
        public StringField birthDate_c1 { get; set; }

        [JsonProperty("162000408")]
        public StringField gradeCompleted_c1 { get; set; }

        [JsonProperty("162000409")]
        public StringField hasAllergies_c1 { get; set; }

        [JsonProperty("162000410")]
        public StringField allergiesDescription_c1 { get; set; }

        [JsonProperty("162000411")]
        public StringField hasMedicalConditions_c1 { get; set; }

        [JsonProperty("162000412")]
        public StringField medicalConditionsDescription_c1 { get; set; }

        [JsonProperty("162000413")]
        public StringField hasMedications_c1 { get; set; }

        [JsonProperty("162000414")]
        public StringField medicationsDescription_c1 { get; set; }

        [JsonProperty("162000415")]
        public StringField placeChildWith_c1 { get; set; }


        [JsonProperty("162000417")]
        public StringField session_c2 { get; set; }

        [JsonProperty("162000418")]
        public NameField name_c2 { get; set; }

        [JsonProperty("162000419")]
        public StringField gender_c2 { get; set; }

        [JsonProperty("162000420")]
        public StringField birthDate_c2 { get; set; }

        [JsonProperty("162000423")]
        public StringField gradeCompleted_c2 { get; set; }

        [JsonProperty("162000424")]
        public StringField hasAllergies_c2 { get; set; }

        [JsonProperty("162000425")]
        public StringField allergiesDescription_c2 { get; set; }

        [JsonProperty("162000426")]
        public StringField hasMedicalConditions_c2 { get; set; }

        [JsonProperty("162000427")]
        public StringField medicalConditionsDescription_c2 { get; set; }

        [JsonProperty("162000428")]
        public StringField hasMedications_c2 { get; set; }

        [JsonProperty("162000429")]
        public StringField medicationsDescription_c2 { get; set; }

        [JsonProperty("162000430")]
        public StringField placeChildWith_c2 { get; set; }


        [JsonProperty("162000432")]
        public StringField session_c3 { get; set; }

        [JsonProperty("162000433")]
        public NameField name_c3 { get; set; }

        [JsonProperty("162000434")]
        public StringField gender_c3 { get; set; }

        [JsonProperty("162000435")]
        public StringField birthDate_c3 { get; set; }

        [JsonProperty("162000438")]
        public StringField gradeCompleted_c3 { get; set; }

        [JsonProperty("162000439")]
        public StringField hasAllergies_c3 { get; set; }

        [JsonProperty("162000440")]
        public StringField allergiesDescription_c3 { get; set; }

        [JsonProperty("162000441")]
        public StringField hasMedicalConditions_c3 { get; set; }

        [JsonProperty("162000442")]
        public StringField medicalConditionsDescription_c3 { get; set; }

        [JsonProperty("162000443")]
        public StringField hasMedications_c3 { get; set; }

        [JsonProperty("162000444")]
        public StringField medicationsDescription_c3 { get; set; }

        [JsonProperty("162000445")]
        public StringField placeChildWith_c3 { get; set; }


        [JsonProperty("162000447")]
        public StringField session_c4 { get; set; }

        [JsonProperty("162000448")]
        public NameField name_c4 { get; set; }

        [JsonProperty("162000449")]
        public StringField gender_c4 { get; set; }

        [JsonProperty("162000450")]
        public StringField birthDate_c4 { get; set; }

        [JsonProperty("162000453")]
        public StringField gradeCompleted_c4 { get; set; }

        [JsonProperty("162000454")]
        public StringField hasAllergies_c4 { get; set; }

        [JsonProperty("162000455")]
        public StringField allergiesDescription_c4 { get; set; }

        [JsonProperty("162000456")]
        public StringField hasMedicalConditions_c4 { get; set; }

        [JsonProperty("162000457")]
        public StringField medicalConditionsDescription_c4 { get; set; }

        [JsonProperty("162000458")]
        public StringField hasMedications_c4 { get; set; }

        [JsonProperty("162000459")]
        public StringField medicationsDescription_c4 { get; set; }

        [JsonProperty("162000460")]
        public StringField placeChildWith_c4 { get; set; }


        [JsonProperty("162000462")]
        public StringField session_c5 { get; set; }

        [JsonProperty("162000463")]
        public NameField name_c5 { get; set; }

        [JsonProperty("162000464")]
        public StringField gender_c5 { get; set; }

        [JsonProperty("162000465")]
        public StringField birthDate_c5 { get; set; }

        [JsonProperty("162000468")]
        public StringField gradeCompleted_c5 { get; set; }

        [JsonProperty("162000469")]
        public StringField hasAllergies_c5 { get; set; }

        [JsonProperty("162000470")]
        public StringField allergiesDescription_c5 { get; set; }

        [JsonProperty("162000471")]
        public StringField hasMedicalConditions_c5 { get; set; }

        [JsonProperty("162000472")]
        public StringField medicalConditionsDescription_c5 { get; set; }

        [JsonProperty("162000473")]
        public StringField hasMedications_c5 { get; set; }

        [JsonProperty("162000474")]
        public StringField medicationsDescription_c5 { get; set; }

        [JsonProperty("162000475")]
        public StringField placeChildWith_c5 { get; set; }

        [JsonProperty("162000477")]
        public CheckboxField consent { get; set; }

        [JsonProperty("162000478")]
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
