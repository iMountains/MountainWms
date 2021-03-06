using FluentValidation;

namespace M.Core.Entity.Fluent.Validation
{
    public class StockOutDetailFluent : AbstractValidator<Wms_stockoutdetail>
    {
        public StockOutDetailFluent()
        {
            RuleFor(x => x.Remark).MaximumLength(200).WithMessage("备注长度不能超过200");
        }
    }
}