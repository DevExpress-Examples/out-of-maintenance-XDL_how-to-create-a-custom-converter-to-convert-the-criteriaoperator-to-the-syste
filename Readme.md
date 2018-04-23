# How to create a custom converter to convert the CriteriaOperator to the System.String type


<p>Sometimes the default way of converting CriteriaOperator into System.String is not acceptable. This example demonstrates how to create your own converter by implementing the ICriteriaVisitor and IClientCriteriaVisitor interfaces.</p>
<p><strong>See </strong><strong>a</strong><strong>lso: </strong><a href="https://www.devexpress.com/Support/Center/p/E3396">How to delete all criteria corresponding to a particular field from CriteriaOperator</a></p>


<h3>Description</h3>

Starting from version 15.1, we introduced the generic version of the ICriteiraVisitor and IClientCriteriaVisitor interfaces: ICriteriaVisitor&lt;T&gt; and IClientCriteriaVisitor&lt;T&gt;. Generic interfaces should be used in all cases where the return value type is important.

<br/>


