using System;
using DevExpress.Data.Filtering;
using System.Collections.Generic;

namespace CriteriaVisitorExample {
    public class CustomCriteriaToStringParser :ICriteriaVisitor, IClientCriteriaVisitor {
        public static string Process(CriteriaOperator op) {
            return op.Accept(new CustomCriteriaToStringParser()).ToString();
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

        object ICriteriaVisitor.Visit(FunctionOperator theOperator) {
            List<string> parameters = new List<string>();
            foreach (CriteriaOperator operand in theOperator.Operands)
                parameters.Add(operand.Accept(this).ToString());
            return string.Format("{0}({1})", GetFunctionName(theOperator.OperatorType),
                string.Join(", ", parameters.ToArray()));
        }

        object ICriteriaVisitor.Visit(OperandValue theOperand) {
            return theOperand.Value == null ? string.Empty : string.Format("'{0}'",
                theOperand.Value);
        }

        object ICriteriaVisitor.Visit(GroupOperator theOperator) {
            List<string> operands = new List<string>();
            foreach (CriteriaOperator operand in theOperator.Operands)
                operands.Add(string.Format("({0})", operand.Accept(this)));
            return string.Join(string.Format(" {0} ", theOperator.OperatorType.ToString()), 
                operands.ToArray());
        }

        object ICriteriaVisitor.Visit(InOperator theOperator) {
            List<string> operands = new List<string>();
            foreach (CriteriaOperator operand in theOperator.Operands)
                operands.Add(operand.Accept(this).ToString());
            return string.Format("{0} in {1}", theOperator.LeftOperand.Accept(this).ToString(),
                string.Join(", ", operands.ToArray()));
        }

        object ICriteriaVisitor.Visit(UnaryOperator theOperator) {
            return string.Format("{0} {1}", GetUnaryOperationName(theOperator.OperatorType),
                theOperator.Operand.Accept(this).ToString());
        }

        object ICriteriaVisitor.Visit(BinaryOperator theOperator) {
            string left = theOperator.LeftOperand.Accept(this).ToString();
            string right = theOperator.RightOperand.Accept(this).ToString();
            return string.Format("{0} {1} {2}", left, GetBinaryOperationName(theOperator.OperatorType),
                right);
        }

        object ICriteriaVisitor.Visit(BetweenOperator theOperator) {
            return string.Format("{0} is between {1} and {2}", theOperator.TestExpression.Accept(this).ToString(),
                theOperator.BeginExpression.Accept(this).ToString(), theOperator.EndExpression.Accept(this).ToString());
        }

        #endregion

        #region IClientCriteriaVisitor Members

        object IClientCriteriaVisitor.Visit(OperandProperty theOperand) {
            return string.Format("[{0}]", theOperand.PropertyName);
        }

        object IClientCriteriaVisitor.Visit(AggregateOperand theOperand) {
            throw new NotImplementedException();
        }

        object IClientCriteriaVisitor.Visit(JoinOperand theOperand) {
            throw new NotImplementedException();
        }

        #endregion
    }
}