using System.Linq.Expressions;

namespace EasyAccess.Infrastructure.Util.ConditionBuilder
{
    public class ParameterExpressionVisitor : ExpressionVisitor
    {
        private readonly ParameterExpression _newParameterExpression;

        public ParameterExpressionVisitor(ParameterExpression parameter)
        {
            _newParameterExpression = parameter;
        }

        public Expression ChangeParameter(Expression expr)
        {
            return Visit(expr);
        }

        protected override Expression VisitParameter(ParameterExpression parameter)
        {
            return _newParameterExpression;
        }
    }
}
