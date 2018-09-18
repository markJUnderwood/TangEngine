using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TangAI.Behavior;
using TangAI.Behavior.Nodes;

namespace TangAI.Tests
{
    [TestClass]
    public class BlackBoardTest
    {
        internal Blackboard BlackBoard { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            BlackBoard = new Blackboard();
            BlackBoard.SetValue(1, "int");
            BlackBoard.SetValue("Foobar", "string");
            BlackBoard.SetValue(new {Id = "Base", Number = 1, Float = 99.999f}, "object");
            BlackBoard.SetValue(11,"int1","Tree1","Node1");
            BlackBoard.SetValue(12,"int1","Tree1","Node2");
            BlackBoard.SetValue(13,"int1","Tree2","Node1");
            BlackBoard.SetValue(14,"int1","Tree2","Node2");
            BlackBoard.SetValue(21,"int2","Tree1","Node1");
            BlackBoard.SetValue(22,"int2","Tree1","Node2");
            BlackBoard.SetValue(23,"int2","Tree2","Node1");
            BlackBoard.SetValue(24,"int2","Tree2","Node2");
            BlackBoard.SetValue("foo1","string1","Tree1","Node2");
            BlackBoard.SetValue("bar1","string1","Tree2","Node1");
            BlackBoard.SetValue("foobar1","string1","Tree2","Node2");
            BlackBoard.SetValue("foobar2","string1","Tree1","Node1");
            BlackBoard.SetValue("foo2","string2","Tree1","Node1");
            BlackBoard.SetValue("bar2","string2","Tree1","Node2");
            BlackBoard.SetValue("foobar12","string2","Tree2","Node1");
            BlackBoard.SetValue("foobar21","string2","Tree2","Node2");

            BlackBoard.SetValue( new {Id = "O1T1N2", Number = 1, Float = 0.8f},"object1","Tree1","Node2");
            BlackBoard.SetValue( new {Id = "O1T2N1", Number = 2, Float = 0.7f},"object1","Tree2","Node1");
            BlackBoard.SetValue( new {Id = "O1T2N2", Number = 3, Float = 0.6f},"object1","Tree2","Node2");
            BlackBoard.SetValue( new {Id = "O1T1N1", Number = 4, Float = 0.5f},"object1","Tree1","Node1");
            BlackBoard.SetValue( new {Id = "O2T1N1", Number = 5, Float = 0.4f},"object2","Tree1","Node1");
            BlackBoard.SetValue( new {Id = "O2T1N2", Number = 6, Float = 0.3f},"object2","Tree1","Node2");
            BlackBoard.SetValue( new {Id = "O2T2N1", Number = 7, Float = 0.2f},"object2","Tree2","Node1");
            BlackBoard.SetValue( new {Id = "O2T2N2", Number = 8, Float = 0.1f},"object2","Tree2","Node2");
        }


        [TestMethod]
        public void TestGet()
        {
            Assert.AreEqual(BlackBoard.GetValue<int>("int"), 1);
            Assert.AreEqual(BlackBoard.GetValue<string>("string"), "Foobar");
            Assert.AreEqual(BlackBoard.GetValue<object>("object"), new {Id = "Base", Number = 1, Float = 99.999f});
            Assert.AreEqual(BlackBoard.GetValue<int>("int1","Tree1","Node1"),11);
            Assert.AreEqual(BlackBoard.GetValue<int>("int1","Tree1","Node2"),12);
            Assert.AreEqual(BlackBoard.GetValue<int>("int1","Tree2","Node1"),13);
            Assert.AreEqual(BlackBoard.GetValue<int>("int1","Tree2","Node2"),14);
            Assert.AreEqual(BlackBoard.GetValue<int>("int2","Tree1","Node1"),21);
            Assert.AreEqual(BlackBoard.GetValue<int>("int2","Tree1","Node2"),22);
            Assert.AreEqual(BlackBoard.GetValue<int>("int2","Tree2","Node1"),23);
            Assert.AreEqual(BlackBoard.GetValue<int>("int2","Tree2","Node2"),24);
            Assert.AreEqual(BlackBoard.GetValue<string>("string1","Tree1","Node2"),"foo1");
            Assert.AreEqual(BlackBoard.GetValue<string>("string1","Tree2","Node1"),"bar1");
            Assert.AreEqual(BlackBoard.GetValue<string>("string1","Tree2","Node2"),"foobar1");
            Assert.AreEqual(BlackBoard.GetValue<string>("string1","Tree1","Node1"),"foobar2");
            Assert.AreEqual(BlackBoard.GetValue<string>("string2","Tree1","Node1"),"foo2");
            Assert.AreEqual(BlackBoard.GetValue<string>("string2","Tree1","Node2"),"bar2");
            Assert.AreEqual(BlackBoard.GetValue<string>("string2","Tree2","Node1"),"foobar12");
            Assert.AreEqual(BlackBoard.GetValue<string>("string2","Tree2","Node2"),"foobar21");

            Assert.AreEqual(BlackBoard.GetValue<object>("object1", "Tree1", "Node2"), new {Id = "O1T1N2", Number = 1, Float = 0.8f});
            Assert.AreEqual(BlackBoard.GetValue<object>("object1", "Tree2", "Node1"), new {Id = "O1T2N1", Number = 2, Float = 0.7f});
            Assert.AreEqual(BlackBoard.GetValue<object>("object1", "Tree2", "Node2"), new {Id = "O1T2N2", Number = 3, Float = 0.6f});
            Assert.AreEqual(BlackBoard.GetValue<object>("object1", "Tree1", "Node1"), new {Id = "O1T1N1", Number = 4, Float = 0.5f});
            Assert.AreEqual(BlackBoard.GetValue<object>("object2", "Tree1", "Node1"), new {Id = "O2T1N1", Number = 5, Float = 0.4f});
            Assert.AreEqual(BlackBoard.GetValue<object>("object2", "Tree1", "Node2"), new {Id = "O2T1N2", Number = 6, Float = 0.3f});
            Assert.AreEqual(BlackBoard.GetValue<object>("object2", "Tree2", "Node1"), new {Id = "O2T2N1", Number = 7, Float = 0.2f});
            Assert.AreEqual(BlackBoard.GetValue<object>("object2", "Tree2", "Node2"), new {Id = "O2T2N2", Number = 8, Float = 0.1f});

            try
            {
                BlackBoard.GetValue<int>("I don't exist");
            }
            catch (KeyNotFoundException)
            {
                Assert.Fail("Key Not found exception thrown on BlackBoard.GetValue<T>(string) when the key doesn't exist");
            }
            try
            {
                BlackBoard.GetValue<int>("I don't exist","InvalidScope","InvalidScope");
                BlackBoard.GetValue<int>("I don't exist","Tree1","InvalidScope");
                BlackBoard.GetValue<int>("I don't exist","InvalidScope","Node1");
                BlackBoard.GetValue<int>("I don't exist","Tree1","Node1");
            }
            catch (KeyNotFoundException)
            {
                Assert.Fail("Key Not found exception thrown on BlackBoard.GetValue<T>(string,string,string)");
            }
        }

        [TestMethod]
        public void TestSet()
        {
            TestGet();
            BlackBoard.SetValue(6,"int");
            BlackBoard.SetValue("Foobar","string");
            BlackBoard.SetValue(new {Id = "Base", Number = 1, Float = 99.999f},"object");
            BlackBoard.SetValue(31,"int1","Tree1","Node1");
            BlackBoard.SetValue(32,"int1","Tree1","Node2");
            BlackBoard.SetValue(33,"int1","Tree2","Node1");
            BlackBoard.SetValue(34,"int1","Tree2","Node2");
            BlackBoard.SetValue(41,"int2","Tree1","Node1");
            BlackBoard.SetValue(42,"int2","Tree1","Node2");
            BlackBoard.SetValue(43,"int2","Tree2","Node1");
            BlackBoard.SetValue(44,"int2","Tree2","Node2");
            BlackBoard.SetValue("2foo1","string1","Tree1","Node2");
            BlackBoard.SetValue("2bar1","string1","Tree2","Node1");
            BlackBoard.SetValue("2foobar1","string1","Tree2","Node2");
            BlackBoard.SetValue("2foobar2","string1","Tree1","Node1");
            BlackBoard.SetValue("2foo2","string2","Tree1","Node1");
            BlackBoard.SetValue("2bar2","string2","Tree1","Node2");
            BlackBoard.SetValue("2foobar12","string2","Tree2","Node1");
            BlackBoard.SetValue("2foobar21","string2","Tree2","Node2");
            BlackBoard.SetValue( new {Id = "2O1T1N2", Number = 21, Float = 10.8f},"object1","Tree1","Node2");
            BlackBoard.SetValue( new {Id = "2O1T2N1", Number = 22, Float = 10.7f},"object1","Tree2","Node1");
            BlackBoard.SetValue( new {Id = "2O1T2N2", Number = 23, Float = 10.6f},"object1","Tree2","Node2");
            BlackBoard.SetValue( new {Id = "2O1T1N1", Number = 24, Float = 10.5f},"object1","Tree1","Node1");
            BlackBoard.SetValue( new {Id = "2O2T1N1", Number = 25, Float = 10.4f},"object2","Tree1","Node1");
            BlackBoard.SetValue( new {Id = "2O2T1N2", Number = 26, Float = 10.3f},"object2","Tree1","Node2");
            BlackBoard.SetValue( new {Id = "2O2T2N1", Number = 27, Float = 10.2f},"object2","Tree2","Node1");
            BlackBoard.SetValue( new {Id = "2O2T2N2", Number = 28, Float = 10.1f},"object2","Tree2","Node2");
            Assert.AreEqual(BlackBoard.GetValue<int>("int"),6);
            Assert.AreEqual(BlackBoard.GetValue<string>("string"), "Foobar");
            Assert.AreEqual(BlackBoard.GetValue<object>("object"), new {Id = "Base", Number = 1, Float = 99.999f});
            Assert.AreEqual(BlackBoard.GetValue<object>("int1","Tree1","Node1"),31);
            Assert.AreEqual(BlackBoard.GetValue<object>("int1","Tree1","Node2"),32);
            Assert.AreEqual(BlackBoard.GetValue<object>("int1","Tree2","Node1"),33);
            Assert.AreEqual(BlackBoard.GetValue<object>("int1","Tree2","Node2"),34);
            Assert.AreEqual(BlackBoard.GetValue<object>("int2","Tree1","Node1"),41);
            Assert.AreEqual(BlackBoard.GetValue<object>("int2","Tree1","Node2"),42);
            Assert.AreEqual(BlackBoard.GetValue<object>("int2","Tree2","Node1"),43);
            Assert.AreEqual(BlackBoard.GetValue<object>("int2","Tree2","Node2"),44);
            Assert.AreEqual(BlackBoard.GetValue<object>("string1","Tree1","Node2"),"2foo1");
            Assert.AreEqual(BlackBoard.GetValue<object>("string1","Tree2","Node1"),"2bar1");
            Assert.AreEqual(BlackBoard.GetValue<object>("string1","Tree2","Node2"),"2foobar1");
            Assert.AreEqual(BlackBoard.GetValue<object>("string1","Tree1","Node1"),"2foobar2");
            Assert.AreEqual(BlackBoard.GetValue<object>("string2","Tree1","Node1"),"2foo2");
            Assert.AreEqual(BlackBoard.GetValue<object>("string2","Tree1","Node2"),"2bar2");
            Assert.AreEqual(BlackBoard.GetValue<object>("string2","Tree2","Node1"),"2foobar12");
            Assert.AreEqual(BlackBoard.GetValue<object>("string2","Tree2","Node2"),"2foobar21");
            Assert.AreEqual(BlackBoard.GetValue<object>("object1", "Tree1", "Node2"), new {Id = "2O1T1N2", Number = 21, Float = 10.8f});
            Assert.AreEqual(BlackBoard.GetValue<object>("object1", "Tree2", "Node1"), new {Id = "2O1T2N1", Number = 22, Float = 10.7f});
            Assert.AreEqual(BlackBoard.GetValue<object>("object1", "Tree2", "Node2"), new {Id = "2O1T2N2", Number = 23, Float = 10.6f});
            Assert.AreEqual(BlackBoard.GetValue<object>("object1", "Tree1", "Node1"), new {Id = "2O1T1N1", Number = 24, Float = 10.5f});
            Assert.AreEqual(BlackBoard.GetValue<object>("object2", "Tree1", "Node1"), new {Id = "2O2T1N1", Number = 25, Float = 10.4f});
            Assert.AreEqual(BlackBoard.GetValue<object>("object2", "Tree1", "Node2"), new {Id = "2O2T1N2", Number = 26, Float = 10.3f});
            Assert.AreEqual(BlackBoard.GetValue<object>("object2", "Tree2", "Node1"), new {Id = "2O2T2N1", Number = 27, Float = 10.2f});
            Assert.AreEqual(BlackBoard.GetValue<object>("object2", "Tree2", "Node2"), new {Id = "2O2T2N2", Number = 28, Float = 10.1f});
        }
    }

    [TestClass]
    public class NodeTests
    {

        [TestMethod]
        [TestCategory("Composite Nodes")]
        [TestCategory("Nodes")]
        public void SequenceTest()
        {
            //Default constructor test
            Sequence n = new Sequence();
            CheckProperties(n);
            n = new Sequence("Foo");
            CheckProperties(n);
            Assert.AreEqual(n.Id, "Foo");
            List<BaseNode> children = new List<BaseNode> {new Inverter()};
            n = new Sequence("Bar", children);
            CheckProperties(n);
            Assert.AreEqual(n.Id, "Bar");
            Assert.AreEqual(children, n.Children);

            children = new List<BaseNode> {new StateReturnNode()};
            n = new Sequence(children);
            Tick tick = new Tick();
            TickNode(n, tick, BehaviorState.Success);
            n.Children.Add(new StateReturnNode());
            TickNode(n, tick, BehaviorState.Success);
            n.Children.Add(new StateReturnNode(BehaviorState.Failure));
            TickNode(n, tick, BehaviorState.Failure);
            n.Children.RemoveAt(n.Children.Count - 1);
            n.Children.Add(new StateReturnNode(BehaviorState.Running));
            TickNode(n, tick, BehaviorState.Running);

            n.Children.RemoveAt(n.Children.Count - 1);
            n.Children.Add(new StateReturnNode(BehaviorState.Error));
            TickNode(n, tick, BehaviorState.Error);
            n.Children.Add(new StateReturnNode(BehaviorState.Failure));
            TickNode(n, tick, BehaviorState.Error);
        }

        [TestMethod]
        [TestCategory("Composite Nodes")]
        [TestCategory("Nodes")]
        public void SelectorTest()
        {
            //Default constructor test
            Selector n = new Selector();
            CheckProperties(n);
            n = new Selector("Foo");
            CheckProperties(n);
            Assert.AreEqual(n.Id, "Foo");
            List<BaseNode> children = new List<BaseNode> {new Inverter()};
            n = new Selector("Bar", children);
            CheckProperties(n);
            Assert.AreEqual(n.Id, "Bar");
            Assert.AreEqual(children, n.Children);

            children = new List<BaseNode> {new StateReturnNode()};
            n = new Selector(children);
            Tick tick = new Tick();
            TickNode(n, tick, BehaviorState.Success);
            n.Children.Add(new StateReturnNode());
            TickNode(n, tick, BehaviorState.Success);
            n.Children.Add(new StateReturnNode(BehaviorState.Failure));
            TickNode(n, tick, BehaviorState.Success);
            n.Children.RemoveAt(n.Children.Count - 1);
            n.Children.Insert(0,new StateReturnNode(BehaviorState.Running));
            TickNode(n, tick, BehaviorState.Running);

            n.Children.RemoveAt(n.Children.Count - 1);
            n.Children.Insert(0, new StateReturnNode(BehaviorState.Failure));
            TickNode(n, tick, BehaviorState.Success);
            n.Children.Insert(0,new StateReturnNode(BehaviorState.Error));
            TickNode(n, tick, BehaviorState.Error);
        }

        private static void TickNode(BaseNode node,Tick tick, BehaviorState expectedResult)
        {
            var result = node.Tick(tick);
            Assert.AreEqual(result, expectedResult);
        }

        private static void CheckProperties(CompositeNode node)
        {
            Assert.IsNotNull(node.Children);
            Assert.IsFalse(string.IsNullOrWhiteSpace(node.Id));
            Assert.IsNotNull(node.Status);
        }
    }
}
