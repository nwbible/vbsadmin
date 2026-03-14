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
        // Mapped to field ids in FormStackForms\2026.html

        [JsonProperty("182014309")]
        public StringField numChildrenRegistered { get; set; }

        [JsonProperty("182014305")]
        public NameField parentGuardianName { get; set; }

        [JsonProperty("182014306")]
        public StringField parentGuardianEmail { get; set; }

        [JsonProperty("182014307")]
        public AddressField address { get; set; }

        [JsonProperty("182014308")]
        public StringField parentGuardianPhone { get; set; }

        [JsonProperty("182014310")]
        public StringField parentGuardianRelationship { get; set; }

        [JsonProperty("182014311")]
        public StringField attendNWB { get; set; }

        [JsonProperty("182014312")]
        public StringField homeChurch { get; set; }

        [JsonProperty("182014313")]
        public StringField invitedBy { get; set; }

        [JsonProperty("182014315")]
        public NameField emergencyContactName { get; set; }

        [JsonProperty("182014316")]
        public StringField emergencyContactPhone { get; set; }

        [JsonProperty("182014317")]
        public StringField emergencyContactRelationship { get; set; }

        // Child pick-up code on the 2026 form
        [JsonProperty("192904692")]
        public StringField password { get; set; }
        
        [JsonProperty("182014319")]
        public StringField session_c1 { get; set; }

        [JsonProperty("182014320")]
        public NameField name_c1 { get; set; }

        [JsonProperty("182014321")]
        public StringField gender_c1 { get; set; }

        [JsonProperty("182014323")]
        public StringField birthDate_c1 { get; set; }

        [JsonProperty("182014326")]
        public StringField gradeCompleted_c1 { get; set; }

        [JsonProperty("182014327")]
        public StringField hasAllergies_c1 { get; set; }

        [JsonProperty("182014328")]
        public StringField allergiesDescription_c1 { get; set; }

        [JsonProperty("182014329")]
        public StringField hasMedicalConditions_c1 { get; set; }

        [JsonProperty("182014330")]
        public StringField medicalConditionsDescription_c1 { get; set; }

        [JsonProperty("182014331")]
        public StringField hasMedications_c1 { get; set; }

        [JsonProperty("182014332")]
        public StringField medicationsDescription_c1 { get; set; }

        [JsonProperty("182014333")]
        public StringField placeChildWith_c1 { get; set; }


        [JsonProperty("182014335")]
        public StringField session_c2 { get; set; }

        [JsonProperty("182014336")]
        public NameField name_c2 { get; set; }

        [JsonProperty("182014337")]
        public StringField gender_c2 { get; set; }

        [JsonProperty("182014338")]
        public StringField birthDate_c2 { get; set; }

        [JsonProperty("182014341")]
        public StringField gradeCompleted_c2 { get; set; }

        [JsonProperty("182014342")]
        public StringField hasAllergies_c2 { get; set; }

        [JsonProperty("182014343")]
        public StringField allergiesDescription_c2 { get; set; }

        [JsonProperty("182014344")]
        public StringField hasMedicalConditions_c2 { get; set; }

        [JsonProperty("182014345")]
        public StringField medicalConditionsDescription_c2 { get; set; }

        [JsonProperty("182014346")]
        public StringField hasMedications_c2 { get; set; }

        [JsonProperty("182014347")]
        public StringField medicationsDescription_c2 { get; set; }

        [JsonProperty("182014348")]
        public StringField placeChildWith_c2 { get; set; }


        [JsonProperty("182014350")]
        public StringField session_c3 { get; set; }

        [JsonProperty("182014351")]
        public NameField name_c3 { get; set; }

        [JsonProperty("182014352")]
        public StringField gender_c3 { get; set; }

        [JsonProperty("182014353")]
        public StringField birthDate_c3 { get; set; }

        [JsonProperty("182014356")]
        public StringField gradeCompleted_c3 { get; set; }

        [JsonProperty("182014357")]
        public StringField hasAllergies_c3 { get; set; }

        [JsonProperty("182014358")]
        public StringField allergiesDescription_c3 { get; set; }

        [JsonProperty("182014359")]
        public StringField hasMedicalConditions_c3 { get; set; }

        [JsonProperty("182014360")]
        public StringField medicalConditionsDescription_c3 { get; set; }

        [JsonProperty("182014361")]
        public StringField hasMedications_c3 { get; set; }

        [JsonProperty("182014362")]
        public StringField medicationsDescription_c3 { get; set; }

        [JsonProperty("182014363")]
        public StringField placeChildWith_c3 { get; set; }


        [JsonProperty("182014365")]
        public StringField session_c4 { get; set; }

        [JsonProperty("182014366")]
        public NameField name_c4 { get; set; }

        [JsonProperty("182014367")]
        public StringField gender_c4 { get; set; }

        [JsonProperty("182014368")]
        public StringField birthDate_c4 { get; set; }

        [JsonProperty("182014371")]
        public StringField gradeCompleted_c4 { get; set; }

        [JsonProperty("182014372")]
        public StringField hasAllergies_c4 { get; set; }

        [JsonProperty("182014373")]
        public StringField allergiesDescription_c4 { get; set; }

        [JsonProperty("182014374")]
        public StringField hasMedicalConditions_c4 { get; set; }

        [JsonProperty("182014375")]
        public StringField medicalConditionsDescription_c4 { get; set; }

        [JsonProperty("182014376")]
        public StringField hasMedications_c4 { get; set; }

        [JsonProperty("182014377")]
        public StringField medicationsDescription_c4 { get; set; }

        [JsonProperty("182014378")]
        public StringField placeChildWith_c4 { get; set; }


        [JsonProperty("182014380")]
        public StringField session_c5 { get; set; }

        [JsonProperty("182014381")]
        public NameField name_c5 { get; set; }

        [JsonProperty("182014382")]
        public StringField gender_c5 { get; set; }

        [JsonProperty("182014383")]
        public StringField birthDate_c5 { get; set; }

        [JsonProperty("182014386")]
        public StringField gradeCompleted_c5 { get; set; }

        [JsonProperty("182014387")]
        public StringField hasAllergies_c5 { get; set; }

        [JsonProperty("182014388")]
        public StringField allergiesDescription_c5 { get; set; }

        [JsonProperty("182014389")]
        public StringField hasMedicalConditions_c5 { get; set; }

        [JsonProperty("182014390")]
        public StringField medicalConditionsDescription_c5 { get; set; }

        [JsonProperty("182014391")]
        public StringField hasMedications_c5 { get; set; }

        [JsonProperty("182014392")]
        public StringField medicationsDescription_c5 { get; set; }

        [JsonProperty("182014393")]
        public StringField placeChildWith_c5 { get; set; }

        [JsonProperty("182014395")]
        public CheckboxField consent { get; set; }

        [JsonProperty("182014396")]
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
        public string address2 { get; set; }
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

    public class TextField
    {
        public string field { get; set; }
        public string flat_value { get; set; }
    }

}
