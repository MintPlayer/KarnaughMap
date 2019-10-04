using System.CodeDom;
using System.ComponentModel.Design.Serialization;

namespace KarnaughMap
{
    internal class KarnaughMapSerializer : CodeDomSerializer
    {
        public override object Serialize(IDesignerSerializationManager manager, object value)
        {
            var userControlSerializer = manager.GetSerializer(typeof(KarnaughMap).BaseType, typeof(CodeDomSerializer)) as CodeDomSerializer;
            if (userControlSerializer == null)
                System.Windows.Forms.MessageBox.Show("userControlSerializer == null");

            var statements = userControlSerializer.Serialize(manager, value) as CodeStatementCollection;
            if (statements == null)
                System.Windows.Forms.MessageBox.Show("statements == null");

            var karnaughMap = value as KarnaughMap;
            if (karnaughMap == null)
            {
                System.Windows.Forms.MessageBox.Show("karnaughMap == null");
                return null;
            }

            if (statements != null)
            {
                var targetObject = base.GetExpression(manager, value);

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
