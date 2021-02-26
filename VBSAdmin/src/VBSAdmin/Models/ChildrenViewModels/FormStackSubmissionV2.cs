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
        [JsonProperty("77839601")]
        public StringField numChildrenRegistered { get; set; }

        [JsonProperty("77839597")]
        public NameField parentGuardianName { get; set; }

        [JsonProperty("77839598")]
        public StringField parentGuardianEmail { get; set; }

        [JsonProperty("77839599")]
        public AddressField address { get; set; }

        [JsonProperty("77839600")]
        public StringField parentGuardianPhone { get; set; }

        [JsonProperty("77839602")]
        public StringField parentGuardianRelationship { get; set; }

        [JsonProperty("77839603")]
        public StringField attendNWB { get; set; }

        [JsonProperty("77839604")]
        public StringField homeChurch { get; set; }

        [JsonProperty("77839605")]
        public StringField invitedBy { get; set; }

        [JsonProperty("77839607")]
        public NameField emergencyContactName { get; set; }

        [JsonProperty("77839608")]
        public StringField emergencyContactPhone { get; set; }

        [JsonProperty("77839609")]
        public StringField emergencyContactRelationship { get; set; }

        [JsonProperty("77839611")]
        public StringField session_c1 { get; set; }

        [JsonProperty("77839612")]
        public NameField name_c1 { get; set; }

        [JsonProperty("77839613")]
        public StringField gender_c1 { get; set; }

        [JsonProperty("77839614")]
        public StringField birthDate_c1 { get; set; }

        [JsonProperty("77839615")]
        public StringField gradeCompleted_c1 { get; set; }

        [JsonProperty("77839616")]
        public StringField hasAllergies_c1 { get; set; }

        [JsonProperty("77839617")]
        public StringField allergiesDescription_c1 { get; set; }

        [JsonProperty("77839618")]
        public StringField hasMedicalConditions_c1 { get; set; }

        [JsonProperty("77839619")]
        public StringField medicalConditionsDescription_c1 { get; set; }

        [JsonProperty("77839620")]
        public StringField hasMedications_c1 { get; set; }

        [JsonProperty("77839621")]
        public StringField medicationsDescription_c1 { get; set; }

        [JsonProperty("77839622")]
        public StringField placeChildWith_c1 { get; set; }


        [JsonProperty("77839624")]
        public StringField session_c2 { get; set; }

        [JsonProperty("77839625")]
        public NameField name_c2 { get; set; }

        [JsonProperty("77839626")]
        public StringField gender_c2 { get; set; }

        [JsonProperty("77839627")]
        public StringField birthDate_c2 { get; set; }

        [JsonProperty("77839628")]
        public StringField gradeCompleted_c2 { get; set; }

        [JsonProperty("77839629")]
        public StringField hasAllergies_c2 { get; set; }

        [JsonProperty("77839630")]
        public StringField allergiesDescription_c2 { get; set; }

        [JsonProperty("77839631")]
        public StringField hasMedicalConditions_c2 { get; set; }

        [JsonProperty("77839632")]
        public StringField medicalConditionsDescription_c2 { get; set; }

        [JsonProperty("77839633")]
        public StringField hasMedications_c2 { get; set; }

        [JsonProperty("77839634")]
        public StringField medicationsDescription_c2 { get; set; }

        [JsonProperty("77839635")]
        public StringField placeChildWith_c2 { get; set; }


        [JsonProperty("77839637")]
        public StringField session_c3 { get; set; }

        [JsonProperty("77839638")]
        public NameField name_c3 { get; set; }

        [JsonProperty("77839639")]
        public StringField gender_c3 { get; set; }

        [JsonProperty("77839640")]
        public StringField birthDate_c3 { get; set; }

        [JsonProperty("77839641")]
        public StringField gradeCompleted_c3 { get; set; }

        [JsonProperty("77839642")]
        public StringField hasAllergies_c3 { get; set; }

        [JsonProperty("77839643")]
        public StringField allergiesDescription_c3 { get; set; }

        [JsonProperty("77839644")]
        public StringField hasMedicalConditions_c3 { get; set; }

        [JsonProperty("77839645")]
        public StringField medicalConditionsDescription_c3 { get; set; }

        [JsonProperty("77839646")]
        public StringField hasMedications_c3 { get; set; }

        [JsonProperty("77839647")]
        public StringField medicationsDescription_c3 { get; set; }

        [JsonProperty("77839648")]
        public StringField placeChildWith_c3 { get; set; }


        [JsonProperty("77839650")]
        public StringField session_c4 { get; set; }

        [JsonProperty("77839651")]
        public NameField name_c4 { get; set; }

        [JsonProperty("77839652")]
        public StringField gender_c4 { get; set; }

        [JsonProperty("77839653")]
        public StringField birthDate_c4 { get; set; }

        [JsonProperty("77839654")]
        public StringField gradeCompleted_c4 { get; set; }

        [JsonProperty("77839655")]
        public StringField hasAllergies_c4 { get; set; }

        [JsonProperty("77839656")]
        public StringField allergiesDescription_c4 { get; set; }

        [JsonProperty("77839657")]
        public StringField hasMedicalConditions_c4 { get; set; }

        [JsonProperty("77839658")]
        public StringField medicalConditionsDescription_c4 { get; set; }

        [JsonProperty("77839659")]
        public StringField hasMedications_c4 { get; set; }

        [JsonProperty("77839660")]
        public StringField medicationsDescription_c4 { get; set; }

        [JsonProperty("77839661")]
        public StringField placeChildWith_c4 { get; set; }


        [JsonProperty("77839663")]
        public StringField session_c5 { get; set; }

        [JsonProperty("77839664")]
        public NameField name_c5 { get; set; }

        [JsonProperty("77839665")]
        public StringField gender_c5 { get; set; }

        [JsonProperty("77839666")]
        public StringField birthDate_c5 { get; set; }

        [JsonProperty("77839667")]
        public StringField gradeCompleted_c5 { get; set; }

        [JsonProperty("77839668")]
        public StringField hasAllergies_c5 { get; set; }

        [JsonProperty("77839669")]
        public StringField allergiesDescription_c5 { get; set; }

        [JsonProperty("77839670")]
        public StringField hasMedicalConditions_c5 { get; set; }

        [JsonProperty("77839671")]
        public StringField medicalConditionsDescription_c5 { get; set; }

        [JsonProperty("77839672")]
        public StringField hasMedications_c5 { get; set; }

        [JsonProperty("77839673")]
        public StringField medicationsDescription_c5 { get; set; }

        [JsonProperty("77839674")]
        public StringField placeChildWith_c5 { get; set; }

        [JsonProperty("77839676")]
        public StringField consent { get; set; }

        [JsonProperty("77839677")]
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
