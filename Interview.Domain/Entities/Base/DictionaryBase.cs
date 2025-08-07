using System;

namespace Interview.Domain.Entities.Base
{
    public abstract class DictionaryBase : BaseEntity
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        
        protected DictionaryBase()
        {
            IsActive = true;
        }
        
        public virtual void Deactivate()
        {
            IsActive = false;
            Update();
        }
        
        public virtual void Activate()
        {
            IsActive = true;
            Update();
        }
    }
}
