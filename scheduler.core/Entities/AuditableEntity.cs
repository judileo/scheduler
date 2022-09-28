﻿using System;

namespace scheduler.core.Entities
{
    public abstract class AuditableEntity
    {
        public DateTime? CreateAt { get; set; }
        public string? CreateBy { get; set; }
        public DateTime? UpdateAt { get; set; }
        public string? UpdateBy { get; set; }
        public DateTime? DeleteAt { get; set; }
        public string? DeleteBy { get; set; }
    }
}
