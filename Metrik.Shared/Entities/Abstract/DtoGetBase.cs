using Metrik.Shared.Utilities.ComplexTypes;

namespace Metrik.Shared.Entities.Abstract
{
    public abstract class DtoGetBase
    {
        public virtual ResultStatus? ResultStatus { get; set; }
        public virtual string? Message { get; set; }
    }
}
