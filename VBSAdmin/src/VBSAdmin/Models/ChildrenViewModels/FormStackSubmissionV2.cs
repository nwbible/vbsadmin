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
        [JsonProperty("52576293")]
        public StringField numChildrenRegistered { get; set; }

        [JsonProperty("52576295")]
        public NameField parentGuardianName { get; set; }

        [JsonProperty("52576296")]
        public StringField parentGuardianEmail { get; set; }

        [JsonProperty("52576297")]
        public AddressField address { get; set; }

        [JsonProperty("52576298")]
        public StringField parentGuardianPhone { get; set; }

        [JsonProperty("52576299")]
        public StringField parentGuardianRelationship { get; set; }

        [JsonProperty("52576300")]
        public StringField attendNWB { get; set; }

        [JsonProperty("52576301")]
        public StringField homeChurch { get; set; }

        [JsonProperty("52576302")]
        public StringField invitedBy { get; set; }

        [JsonProperty("52576304")]
        public NameField emergencyContactName { get; set; }

        [JsonProperty("52576305")]
        public StringField emergencyContactPhone { get; set; }

        [JsonProperty("52576306")]
        public StringField emergencyContactRelationship { get; set; }

        [JsonProperty("52576308")]
        public StringField session_c1 { get; set; }

        [JsonProperty("52576309")]
        public NameField name_c1 { get; set; }

        [JsonProperty("52576310")]
        public StringField gender_c1 { get; set; }

        [JsonProperty("52576311")]
        public StringField birthDate_c1 { get; set; }

        [JsonProperty("52576312")]
        public StringField gradeCompleted_c1 { get; set; }

        [JsonProperty("52576313")]
        public StringField hasAllergies_c1 { get; set; }

        [JsonProperty("52576314")]
        public StringField allergiesDescription_c1 { get; set; }

        [JsonProperty("52576315")]
        public StringField hasMedicalConditions_c1 { get; set; }

        [JsonProperty("52576316")]
        public StringField medicalConditionsDescription_c1 { get; set; }

        [JsonProperty("52576317")]
        public StringField hasMedications_c1 { get; set; }

        [JsonProperty("52576318")]
        public StringField medicationsDescription_c1 { get; set; }

        [JsonProperty("52576319")]
        public StringField placeChildWith_c1 { get; set; }


        [JsonProperty("52576321")]
        public StringField session_c2 { get; set; }

        [JsonProperty("52576322")]
        public NameField name_c2 { get; set; }

        [JsonProperty("52576323")]
        public StringField gender_c2 { get; set; }

        [JsonProperty("52576324")]
        public StringField birthDate_c2 { get; set; }

        [JsonProperty("52576325")]
        public StringField gradeCompleted_c2 { get; set; }

        [JsonProperty("52576326")]
        public StringField hasAllergies_c2 { get; set; }

        [JsonProperty("52576327")]
        public StringField allergiesDescription_c2 { get; set; }

        [JsonProperty("52576328")]
        public StringField hasMedicalConditions_c2 { get; set; }

        [JsonProperty("52576329")]
        public StringField medicalConditionsDescription_c2 { get; set; }

        [JsonProperty("52576330")]
        public StringField hasMedications_c2 { get; set; }

        [JsonProperty("52576331")]
        public StringField medicationsDescription_c2 { get; set; }

        [JsonProperty("52576332")]
        public StringField placeChildWith_c2 { get; set; }


        [JsonProperty("52576334")]
        public StringField session_c3 { get; set; }

        [JsonProperty("52576335")]
        public NameField name_c3 { get; set; }

        [JsonProperty("52576336")]
        public StringField gender_c3 { get; set; }

        [JsonProperty("52576337")]
        public StringField birthDate_c3 { get; set; }

        [JsonProperty("52576338")]
        public StringField gradeCompleted_c3 { get; set; }

        [JsonProperty("52576339")]
        public StringField hasAllergies_c3 { get; set; }

        [JsonProperty("52576340")]
        public StringField allergiesDescription_c3 { get; set; }

        [JsonProperty("52576341")]
        public StringField hasMedicalConditions_c3 { get; set; }

        [JsonProperty("52576342")]
        public StringField medicalConditionsDescription_c3 { get; set; }

        [JsonProperty("52576343")]
        public StringField hasMedications_c3 { get; set; }

        [JsonProperty("52576344")]
        public StringField medicationsDescription_c3 { get; set; }

        [JsonProperty("52576345")]
        public StringField placeChildWith_c3 { get; set; }


        [JsonProperty("52576347")]
        public StringField session_c4 { get; set; }

        [JsonProperty("52576348")]
        public NameField name_c4 { get; set; }

        [JsonProperty("52576349")]
        public StringField gender_c4 { get; set; }

        [JsonProperty("52576350")]
        public StringField birthDate_c4 { get; set; }

        [JsonProperty("52576351")]
        public StringField gradeCompleted_c4 { get; set; }

        [JsonProperty("52576352")]
        public StringField hasAllergies_c4 { get; set; }

        [JsonProperty("52576353")]
        public StringField allergiesDescription_c4 { get; set; }

        [JsonProperty("52576354")]
        public StringField hasMedicalConditions_c4 { get; set; }

        [JsonProperty("52576355")]
        public StringField medicalConditionsDescription_c4 { get; set; }

        [JsonProperty("52576356")]
        public StringField hasMedications_c4 { get; set; }

        [JsonProperty("52576357")]
        public StringField medicationsDescription_c4 { get; set; }

        [JsonProperty("52576358")]
        public StringField placeChildWith_c4 { get; set; }


        [JsonProperty("52576360")]
        public StringField session_c5 { get; set; }

        [JsonProperty("52576361")]
        public NameField name_c5 { get; set; }

        [JsonProperty("52576362")]
        public StringField gender_c5 { get; set; }

        [JsonProperty("52576363")]
        public StringField birthDate_c5 { get; set; }

        [JsonProperty("52576364")]
        public StringField gradeCompleted_c5 { get; set; }

        [JsonProperty("52576365")]
        public StringField hasAllergies_c5 { get; set; }

        [JsonProperty("52576366")]
        public StringField allergiesDescription_c5 { get; set; }

        [JsonProperty("52576367")]
        public StringField hasMedicalConditions_c5 { get; set; }

        [JsonProperty("52576368")]
        public StringField medicalConditionsDescription_c5 { get; set; }

        [JsonProperty("52576369")]
        public StringField hasMedications_c5 { get; set; }

        [JsonProperty("52576370")]
        public StringField medicationsDescription_c5 { get; set; }

        [JsonProperty("52576371")]
        public StringField placeChildWith_c5 { get; set; }

        [JsonProperty("52576373")]
        public StringField consent { get; set; }

        [JsonProperty("52576374")]
        public StringField signatureImageUrl { get; set; }

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
