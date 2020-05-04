namespace GUITestsEngine.Utils
{
    public class TestInfo
    {
        public string MethodName { get; private set; }

        public bool IsPassed { get; private set; }

        public TestInfo(string name, bool isPassed)
        {
            MethodName = name;
            IsPassed = isPassed;
        }
    }
}
