using NUnit.Framework;

namespace Photofeud
{
    public class ScreenStackTests
    {
        readonly Screen _screen;
        readonly ScreenStack _stack;

        public ScreenStackTests()
        {
            _screen = new Screen();
            _stack = new ScreenStack(_screen);
        }

        [Test]
        public void Should_Add_Screen_To_Stack()
        {
            _stack.Clear();

            _stack.Add(_screen);

            Assert.IsNotNull(_stack.Screens);
            Assert.IsTrue(_stack.Screens.Count == 1);
        }

        [Test]
        public void Should_Not_Add_Screen_To_Stack_If_Found()
        {
            _stack.Clear();

            _stack.Add(_screen);
            _stack.Add(_screen);

            Assert.IsNotNull(_stack.Screens);
            Assert.IsTrue(_stack.Screens.Count == 1);
        }

        [Test]
        public void Should_Remove_Screen_From_Stack()
        {
            _stack.Clear();

            _stack.Add(_screen);
            _stack.Remove(_screen);

            Assert.IsNotNull(_stack.Screens);
            Assert.IsTrue(_stack.Screens.Count == 0);
        }

        [Test]
        public void Should_Not_Remove_Screen_From_Stack_If_Not_Found()
        {
            _stack.Clear();

            _stack.Add(_screen);
            _stack.Remove(_screen);
            _stack.Remove(_screen);

            Assert.IsNotNull(_stack.Screens);
            Assert.IsTrue(_stack.Screens.Count == 0);
        }
    }
}
