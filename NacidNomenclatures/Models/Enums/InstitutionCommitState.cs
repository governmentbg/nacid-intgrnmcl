﻿using System.ComponentModel;

namespace Models.Enums
{
    public enum InstitutionCommitState
    {
        [Description("първоначална чернова")]
        InitialDraft = 1,

        [Description("в процес на промяна")]
        Modification = 2,

        [Description("актуално състояние")]
        Actual = 3,

        [Description("актуално с инициирана промяна")]
        ActualWithModification = 4,

        [Description("предишно състояние")]
        History = 5,

        [Description("заличено")]
        Deleted = 6,

        [Description("готов за вписване")]
        CommitReady = 7,

        [Description("заявено за заличаване")]
        ModificationErase = 8,

        [Description("Заявено за възстановяване")]
        ModificationRevertErase = 9
    }
}
