Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic

Namespace CriteriaVisitorExample
	Public Class CustomCriteriaToStringParser
		Implements ICriteriaVisitor, IClientCriteriaVisitor
		Public Shared Function Process(ByVal op As CriteriaOperator) As String
			Return op.Accept(New CustomCriteriaToStringParser()).ToString()
		End Function

		Private Shared Function GetFunctionName(ByVal operatorType As FunctionOperatorType) As String
			Select Case operatorType
				Case FunctionOperatorType.Concat
					Return "Concat"
				Case FunctionOperatorType.Iif
					Return "Iif"
				Case FunctionOperatorType.IsNull
					Return "Is null"
				Case Else
					Return operatorType.ToString()
			End Select
		End Function

		Private Shared Function GetUnaryOperationName(ByVal operatorType As UnaryOperatorType) As String
			Select Case operatorType
				Case UnaryOperatorType.BitwiseNot
					Return "Not"
				Case UnaryOperatorType.IsNull
					Return "Is Null"
				Case UnaryOperatorType.Minus
					Return "Minus"
				Case UnaryOperatorType.Not
					Return "Not"
				Case UnaryOperatorType.Plus
					Return "Plus"
				Case Else
					Return String.Empty
			End Select
		End Function

		Private Shared Function GetBinaryOperationName(ByVal operatorType As BinaryOperatorType) As String
			Select Case operatorType
				Case BinaryOperatorType.Equal
					Return "equals"
				Case BinaryOperatorType.Less
					Return "is less"
				Case BinaryOperatorType.LessOrEqual
					Return "is less than or equals"
				Case BinaryOperatorType.Greater
					Return "is greater than"
				Case BinaryOperatorType.GreaterOrEqual
					Return "is greater than or equals"
				Case Else
					Return operatorType.ToString()
			End Select
		End Function


#Region "ICriteriaVisitor Members"

		Private Function ICriteriaVisitor_Visit(ByVal theOperator As FunctionOperator) As Object Implements ICriteriaVisitor.Visit
			Dim parameters As New List(Of String)()
			For Each operand As CriteriaOperator In theOperator.Operands
				parameters.Add(operand.Accept(Me).ToString())
			Next operand
			Return String.Format("{0}({1})", GetFunctionName(theOperator.OperatorType), String.Join(", ", parameters.ToArray()))
		End Function

		Private Function ICriteriaVisitor_Visit(ByVal theOperand As OperandValue) As Object Implements ICriteriaVisitor.Visit
			If theOperand.Value Is Nothing Then
				Return String.Empty
			Else
				Return String.Format("'{0}'", theOperand.Value)
			End If
		End Function

		Private Function ICriteriaVisitor_Visit(ByVal theOperator As GroupOperator) As Object Implements ICriteriaVisitor.Visit
			Dim operands As New List(Of String)()
			For Each operand As CriteriaOperator In theOperator.Operands
				operands.Add(String.Format("({0})", operand.Accept(Me)))
			Next operand
			Return String.Join(String.Format(" {0} ", theOperator.OperatorType.ToString()), operands.ToArray())
		End Function

		Private Function ICriteriaVisitor_Visit(ByVal theOperator As InOperator) As Object Implements ICriteriaVisitor.Visit
			Dim operands As New List(Of String)()
			For Each operand As CriteriaOperator In theOperator.Operands
				operands.Add(operand.Accept(Me).ToString())
			Next operand
			Return String.Format("{0} in {1}", theOperator.LeftOperand.Accept(Me).ToString(), String.Join(", ", operands.ToArray()))
		End Function

		Private Function ICriteriaVisitor_Visit(ByVal theOperator As UnaryOperator) As Object Implements ICriteriaVisitor.Visit
			Return String.Format("{0} {1}", GetUnaryOperationName(theOperator.OperatorType), theOperator.Operand.Accept(Me).ToString())
		End Function

		Private Function ICriteriaVisitor_Visit(ByVal theOperator As BinaryOperator) As Object Implements ICriteriaVisitor.Visit
			Dim left As String = theOperator.LeftOperand.Accept(Me).ToString()
			Dim right As String = theOperator.RightOperand.Accept(Me).ToString()
			Return String.Format("{0} {1} {2}", left, GetBinaryOperationName(theOperator.OperatorType), right)
		End Function

		Private Function ICriteriaVisitor_Visit(ByVal theOperator As BetweenOperator) As Object Implements ICriteriaVisitor.Visit
			Return String.Format("{0} is between {1} and {2}", theOperator.TestExpression.Accept(Me).ToString(), theOperator.BeginExpression.Accept(Me).ToString(), theOperator.EndExpression.Accept(Me).ToString())
		End Function

		#End Region

		#Region "IClientCriteriaVisitor Members"

		Private Function IClientCriteriaVisitor_Visit(ByVal theOperand As OperandProperty) As Object Implements IClientCriteriaVisitor.Visit
			Return String.Format("[{0}]", theOperand.PropertyName)
		End Function

		Private Function IClientCriteriaVisitor_Visit(ByVal theOperand As AggregateOperand) As Object Implements IClientCriteriaVisitor.Visit
			Throw New NotImplementedException()
		End Function

		Private Function IClientCriteriaVisitor_Visit(ByVal theOperand As JoinOperand) As Object Implements IClientCriteriaVisitor.Visit
			Throw New NotImplementedException()
		End Function

		#End Region
	End Class
End Namespace