using System;
using DevExpress.Data.Filtering;
using System.Collections.Generic;

namespace CriteriaVisitorExample {
    public class CustomCriteriaToStringParser :ICriteriaVisitor<string>, IClientCriteriaVisitor<string> {
        public static string Process(CriteriaOperator op) {
            return op.Accept(new CustomCriteriaToStringParser());
        }

        static string GetFunctionName(FunctionOperatorType operatorType) {
            switch (operatorType) {
                case FunctionOperatorType.Concat: return "Concat";
                case FunctionOperatorType.Iif: return "Iif";
                case FunctionOperatorType.IsNull: return "Is null";
                default: return operatorType.ToString();
            }
        }

        static string GetUnaryOperationName(UnaryOperatorType operatorType) {
            switch (operatorType) {
                case UnaryOperatorType.BitwiseNot: return "Not";
                case UnaryOperatorType.IsNull: return "Is Null";
                case UnaryOperatorType.Minus: return "Minus";
                case UnaryOperatorType.Not: return "Not";
                case UnaryOperatorType.Plus: return "Plus";
                default: return string.Empty;
            }
        }

        static string GetBinaryOperationName(BinaryOperatorType operatorType) {
            switch (operatorType) {
                case BinaryOperatorType.Equal: return "equals";
                case BinaryOperatorType.Less: return "is less";
                case BinaryOperatorType.LessOrEqual: return "is less than or equals";
                case BinaryOperatorType.Greater: return "is greater than";
                case BinaryOperatorType.GreaterOrEqual: return "is greater than or equals";
                default: return operatorType.ToString();
            }
        }


#region ICriteriaVisitor Members

        string ICriteriaVisitor<string>.Visit(FunctionOperator theOperator) {
            List<string> parameters = new List<string>();
            foreach (CriteriaOperator operand in theOperator.Operands)
                parameters.Add(operand.Accept(this));
            return string.Format("{0}({1})", GetFunctionName(theOperator.OperatorType),
                string.Join(", ", parameters.ToArray()));
        }

        string ICriteriaVisitor<string>.Visit(OperandValue theOperand) {
            return theOperand.Value == null ? string.Empty : string.Format("'{0}'",
                theOperand.Value);
        }

        string ICriteriaVisitor<string>.Visit(GroupOperator theOperator) {
            List<string> operands = new List<string>();
            foreach (CriteriaOperator operand in theOperator.Operands)
                operands.Add(string.Format("({0})", operand.Accept(this)));
            return string.Join(string.Format(" {0} ", theOperator.OperatorType), 
                operands.ToArray());
        }

        string ICriteriaVisitor<string>.Visit(InOperator theOperator) {
            List<string> operands = new List<string>();
            foreach (CriteriaOperator operand in theOperator.Operands)
                operands.Add(operand.Accept(this));
            return string.Format("{0} in {1}", theOperator.LeftOperand.Accept(this),
                string.Join(", ", operands.ToArray()));
        }

        string ICriteriaVisitor<string>.Visit(UnaryOperator theOperator) {
            return string.Format("{0} {1}", GetUnaryOperationName(theOperator.OperatorType),
                theOperator.Operand.Accept(this));
        }

        string ICriteriaVisitor<string>.Visit(BinaryOperator theOperator) {
            string left = theOperator.LeftOperand.Accept(this);
            string right = theOperator.RightOperand.Accept(this);
            return string.Format("{0} {1} {2}", left, GetBinaryOperationName(theOperator.OperatorType),
                right);
        }

        string ICriteriaVisitor<string>.Visit(BetweenOperator theOperator) {
            return string.Format("{0} is between {1} and {2}", theOperator.TestExpression.Accept(this),
                theOperator.BeginExpression.Accept(this), theOperator.EndExpression.Accept(this));
        }

        #endregion

        #region IClientCriteriaVisitor Members

        string IClientCriteriaVisitor<string>.Visit(OperandProperty theOperand) {
            return string.Format("[{0}]", theOperand.PropertyName);
        }

        string IClientCriteriaVisitor<string>.Visit(AggregateOperand theOperand) {
            throw new NotImplementedException();
        }

        string IClientCriteriaVisitor<string>.Visit(JoinOperand theOperand) {
            throw new NotImplementedException();
        }

        #endregion
    }
}