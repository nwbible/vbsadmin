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
        [JsonProperty("64656592")]
        public StringField numChildrenRegistered { get; set; }

        [JsonProperty("64656588")]
        public NameField parentGuardianName { get; set; }

        [JsonProperty("64656589")]
        public StringField parentGuardianEmail { get; set; }

        [JsonProperty("64656590")]
        public AddressField address { get; set; }

        [JsonProperty("64656591")]
        public StringField parentGuardianPhone { get; set; }

        [JsonProperty("64656593")]
        public StringField parentGuardianRelationship { get; set; }

        [JsonProperty("64656594")]
        public StringField attendNWB { get; set; }

        [JsonProperty("64656595")]
        public StringField homeChurch { get; set; }

        [JsonProperty("64656596")]
        public StringField invitedBy { get; set; }

        [JsonProperty("64656598")]
        public NameField emergencyContactName { get; set; }

        [JsonProperty("64656599")]
        public StringField emergencyContactPhone { get; set; }

        [JsonProperty("64656600")]
        public StringField emergencyContactRelationship { get; set; }

        [JsonProperty("64656602")]
        public StringField session_c1 { get; set; }

        [JsonProperty("64656603")]
        public NameField name_c1 { get; set; }

        [JsonProperty("64656604")]
        public StringField gender_c1 { get; set; }

        [JsonProperty("64656605")]
        public StringField birthDate_c1 { get; set; }

        [JsonProperty("64656606")]
        public StringField gradeCompleted_c1 { get; set; }

        [JsonProperty("64656607")]
        public StringField hasAllergies_c1 { get; set; }

        [JsonProperty("64656608")]
        public StringField allergiesDescription_c1 { get; set; }

        [JsonProperty("64656609")]
        public StringField hasMedicalConditions_c1 { get; set; }

        [JsonProperty("64656610")]
        public StringField medicalConditionsDescription_c1 { get; set; }

        [JsonProperty("64656611")]
        public StringField hasMedications_c1 { get; set; }

        [JsonProperty("64656612")]
        public StringField medicationsDescription_c1 { get; set; }

        [JsonProperty("64656613")]
        public StringField placeChildWith_c1 { get; set; }


        [JsonProperty("64656615")]
        public StringField session_c2 { get; set; }

        [JsonProperty("64656616")]
        public NameField name_c2 { get; set; }

        [JsonProperty("64656617")]
        public StringField gender_c2 { get; set; }

        [JsonProperty("64656618")]
        public StringField birthDate_c2 { get; set; }

        [JsonProperty("64656619")]
        public StringField gradeCompleted_c2 { get; set; }

        [JsonProperty("64656620")]
        public StringField hasAllergies_c2 { get; set; }

        [JsonProperty("64656621")]
        public StringField allergiesDescription_c2 { get; set; }

        [JsonProperty("64656622")]
        public StringField hasMedicalConditions_c2 { get; set; }

        [JsonProperty("64656623")]
        public StringField medicalConditionsDescription_c2 { get; set; }

        [JsonProperty("64656624")]
        public StringField hasMedications_c2 { get; set; }

        [JsonProperty("64656625")]
        public StringField medicationsDescription_c2 { get; set; }

        [JsonProperty("64656626")]
        public StringField placeChildWith_c2 { get; set; }


        [JsonProperty("64656628")]
        public StringField session_c3 { get; set; }

        [JsonProperty("64656629")]
        public NameField name_c3 { get; set; }

        [JsonProperty("64656630")]
        public StringField gender_c3 { get; set; }

        [JsonProperty("64656631")]
        public StringField birthDate_c3 { get; set; }

        [JsonProperty("64656632")]
        public StringField gradeCompleted_c3 { get; set; }

        [JsonProperty("64656633")]
        public StringField hasAllergies_c3 { get; set; }

        [JsonProperty("64656634")]
        public StringField allergiesDescription_c3 { get; set; }

        [JsonProperty("64656635")]
        public StringField hasMedicalConditions_c3 { get; set; }

        [JsonProperty("64656636")]
        public StringField medicalConditionsDescription_c3 { get; set; }

        [JsonProperty("64656637")]
        public StringField hasMedications_c3 { get; set; }

        [JsonProperty("64656638")]
        public StringField medicationsDescription_c3 { get; set; }

        [JsonProperty("64656639")]
        public StringField placeChildWith_c3 { get; set; }


        [JsonProperty("64656641")]
        public StringField session_c4 { get; set; }

        [JsonProperty("64656642")]
        public NameField name_c4 { get; set; }

        [JsonProperty("64656643")]
        public StringField gender_c4 { get; set; }

        [JsonProperty("64656644")]
        public StringField birthDate_c4 { get; set; }

        [JsonProperty("64656645")]
        public StringField gradeCompleted_c4 { get; set; }

        [JsonProperty("64656646")]
        public StringField hasAllergies_c4 { get; set; }

        [JsonProperty("64656647")]
        public StringField allergiesDescription_c4 { get; set; }

        [JsonProperty("64656648")]
        public StringField hasMedicalConditions_c4 { get; set; }

        [JsonProperty("64656649")]
        public StringField medicalConditionsDescription_c4 { get; set; }

        [JsonProperty("64656650")]
        public StringField hasMedications_c4 { get; set; }

        [JsonProperty("64656651")]
        public StringField medicationsDescription_c4 { get; set; }

        [JsonProperty("64656652")]
        public StringField placeChildWith_c4 { get; set; }


        [JsonProperty("64656654")]
        public StringField session_c5 { get; set; }

        [JsonProperty("64656655")]
        public NameField name_c5 { get; set; }

        [JsonProperty("64656656")]
        public StringField gender_c5 { get; set; }

        [JsonProperty("64656657")]
        public StringField birthDate_c5 { get; set; }

        [JsonProperty("64656658")]
        public StringField gradeCompleted_c5 { get; set; }

        [JsonProperty("64656659")]
        public StringField hasAllergies_c5 { get; set; }

        [JsonProperty("64656660")]
        public StringField allergiesDescription_c5 { get; set; }

        [JsonProperty("64656661")]
        public StringField hasMedicalConditions_c5 { get; set; }

        [JsonProperty("64656662")]
        public StringField medicalConditionsDescription_c5 { get; set; }

        [JsonProperty("64656663")]
        public StringField hasMedications_c5 { get; set; }

        [JsonProperty("64656664")]
        public StringField medicationsDescription_c5 { get; set; }

        [JsonProperty("64656665")]
        public StringField placeChildWith_c5 { get; set; }

        [JsonProperty("64656667")]
        public StringField consent { get; set; }

        [JsonProperty("64656668")]
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
