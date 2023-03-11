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
        [JsonProperty("123583662")]
        public StringField numChildrenRegistered { get; set; }

        [JsonProperty("123583658")]
        public NameField parentGuardianName { get; set; }

        [JsonProperty("123583659")]
        public StringField parentGuardianEmail { get; set; }

        [JsonProperty("123583660")]
        public AddressField address { get; set; }

        [JsonProperty("123583661")]
        public StringField parentGuardianPhone { get; set; }

        [JsonProperty("123583663")]
        public StringField parentGuardianRelationship { get; set; }

        [JsonProperty("123583664")]
        public StringField attendNWB { get; set; }

        [JsonProperty("123583665")]
        public StringField homeChurch { get; set; }

        [JsonProperty("123583666")]
        public StringField invitedBy { get; set; }

        [JsonProperty("123583668")]
        public NameField emergencyContactName { get; set; }

        [JsonProperty("123583669")]
        public StringField emergencyContactPhone { get; set; }

        [JsonProperty("123583670")]
        public StringField emergencyContactRelationship { get; set; }

        [JsonProperty("123583672")]
        public StringField session_c1 { get; set; }

        [JsonProperty("123583673")]
        public NameField name_c1 { get; set; }

        [JsonProperty("123583674")]
        public StringField gender_c1 { get; set; }

        [JsonProperty("123583675")]
        public StringField birthDate_c1 { get; set; }

        [JsonProperty("123583676")]
        public StringField gradeCompleted_c1 { get; set; }

        [JsonProperty("123583677")]
        public StringField hasAllergies_c1 { get; set; }

        [JsonProperty("123583678")]
        public StringField allergiesDescription_c1 { get; set; }

        [JsonProperty("123583679")]
        public StringField hasMedicalConditions_c1 { get; set; }

        [JsonProperty("123583680")]
        public StringField medicalConditionsDescription_c1 { get; set; }

        [JsonProperty("123583681")]
        public StringField hasMedications_c1 { get; set; }

        [JsonProperty("123583682")]
        public StringField medicationsDescription_c1 { get; set; }

        [JsonProperty("123583683")]
        public StringField placeChildWith_c1 { get; set; }


        [JsonProperty("123583685")]
        public StringField session_c2 { get; set; }

        [JsonProperty("123583686")]
        public NameField name_c2 { get; set; }

        [JsonProperty("123583687")]
        public StringField gender_c2 { get; set; }

        [JsonProperty("123583688")]
        public StringField birthDate_c2 { get; set; }

        [JsonProperty("123583689")]
        public StringField gradeCompleted_c2 { get; set; }

        [JsonProperty("123583690")]
        public StringField hasAllergies_c2 { get; set; }

        [JsonProperty("123583691")]
        public StringField allergiesDescription_c2 { get; set; }

        [JsonProperty("123583692")]
        public StringField hasMedicalConditions_c2 { get; set; }

        [JsonProperty("123583693")]
        public StringField medicalConditionsDescription_c2 { get; set; }

        [JsonProperty("123583694")]
        public StringField hasMedications_c2 { get; set; }

        [JsonProperty("123583695")]
        public StringField medicationsDescription_c2 { get; set; }

        [JsonProperty("123583696")]
        public StringField placeChildWith_c2 { get; set; }


        [JsonProperty("123583698")]
        public StringField session_c3 { get; set; }

        [JsonProperty("123583699")]
        public NameField name_c3 { get; set; }

        [JsonProperty("123583700")]
        public StringField gender_c3 { get; set; }

        [JsonProperty("123583701")]
        public StringField birthDate_c3 { get; set; }

        [JsonProperty("123583702")]
        public StringField gradeCompleted_c3 { get; set; }

        [JsonProperty("123583703")]
        public StringField hasAllergies_c3 { get; set; }

        [JsonProperty("123583704")]
        public StringField allergiesDescription_c3 { get; set; }

        [JsonProperty("123583705")]
        public StringField hasMedicalConditions_c3 { get; set; }

        [JsonProperty("123583706")]
        public StringField medicalConditionsDescription_c3 { get; set; }

        [JsonProperty("123583707")]
        public StringField hasMedications_c3 { get; set; }

        [JsonProperty("123583708")]
        public StringField medicationsDescription_c3 { get; set; }

        [JsonProperty("123583709")]
        public StringField placeChildWith_c3 { get; set; }


        [JsonProperty("123583711")]
        public StringField session_c4 { get; set; }

        [JsonProperty("123583712")]
        public NameField name_c4 { get; set; }

        [JsonProperty("123583713")]
        public StringField gender_c4 { get; set; }

        [JsonProperty("123583714")]
        public StringField birthDate_c4 { get; set; }

        [JsonProperty("123583715")]
        public StringField gradeCompleted_c4 { get; set; }

        [JsonProperty("123583716")]
        public StringField hasAllergies_c4 { get; set; }

        [JsonProperty("123583717")]
        public StringField allergiesDescription_c4 { get; set; }

        [JsonProperty("123583718")]
        public StringField hasMedicalConditions_c4 { get; set; }

        [JsonProperty("123583719")]
        public StringField medicalConditionsDescription_c4 { get; set; }

        [JsonProperty("123583720")]
        public StringField hasMedications_c4 { get; set; }

        [JsonProperty("123583721")]
        public StringField medicationsDescription_c4 { get; set; }

        [JsonProperty("123583722")]
        public StringField placeChildWith_c4 { get; set; }


        [JsonProperty("123583724")]
        public StringField session_c5 { get; set; }

        [JsonProperty("123583725")]
        public NameField name_c5 { get; set; }

        [JsonProperty("123583726")]
        public StringField gender_c5 { get; set; }

        [JsonProperty("123583727")]
        public StringField birthDate_c5 { get; set; }

        [JsonProperty("123583728")]
        public StringField gradeCompleted_c5 { get; set; }

        [JsonProperty("123583729")]
        public StringField hasAllergies_c5 { get; set; }

        [JsonProperty("123583730")]
        public StringField allergiesDescription_c5 { get; set; }

        [JsonProperty("123583731")]
        public StringField hasMedicalConditions_c5 { get; set; }

        [JsonProperty("123583732")]
        public StringField medicalConditionsDescription_c5 { get; set; }

        [JsonProperty("123583733")]
        public StringField hasMedications_c5 { get; set; }

        [JsonProperty("123583734")]
        public StringField medicationsDescription_c5 { get; set; }

        [JsonProperty("123583735")]
        public StringField placeChildWith_c5 { get; set; }

        [JsonProperty("123583737")]
        public CheckboxField consent { get; set; }

        [JsonProperty("123583738")]
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
