using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    internal class KarnaughMapSerializer : CodeDomSerializer
    {
        public override object Serialize(IDesignerSerializationManager manager, object value)
        {
            var userControlSerializer = (CodeDomSerializer)manager.GetSerializer(typeof(KarnaughMap).BaseType, typeof(CodeDomSerializer));
            var statements = userControlSerializer.Serialize(manager, value) as CodeStatementCollection;
            var karnaughMap = value as KarnaughMap;
            if (statements != null)
            {
                var targetObject = base.GetExpression(manager, value);

                //var forLoop = new CodeIterationStatement(
                //    new CodeVariableDeclarationStatement(
                //        typeof(int),
                //        "i",
                //        new CodePrimitiveExpression(0)
                //    ),
                //    new CodeBinaryOperatorExpression(
                //        new CodeVariableReferenceExpression("i"),
                //        CodeBinaryOperatorType.LessThan,
                //        new CodePropertyReferenceExpression(
                //            new CodePropertyReferenceExpression(
                //                targetObject,
                //                "InputVariables"
                //            ),
                //            "Count"
                //        )
                //    ),
                //    new CodeAssignStatement(
                //        new CodeVariableReferenceExpression("i"),
                //        new CodeBinaryOperatorExpression(
                //            new CodeVariableReferenceExpression("i"),
                //            CodeBinaryOperatorType.Add,
                //            new CodePrimitiveExpression(1)
                //        )
                //    ),
                //    new CodeExpressionStatement(
                //        new CodeMethodInvokeExpression(
                //            new CodePropertyReferenceExpression(targetObject, "InputVariables"),
                //            "Add",
                //            new CodeArrayIndexerExpression(
                //        )
                //    )
                //);


                foreach (var input in karnaughMap.InputVariables)
                {
                    statements.Add(
                        new CodeMethodInvokeExpression(
                            new CodePropertyReferenceExpression(targetObject, "InputVariables"),
                            "Add",
                            new CodePrimitiveExpression(input)
                        )
                    );
                }

            }
            return statements;
        }
    }
}
