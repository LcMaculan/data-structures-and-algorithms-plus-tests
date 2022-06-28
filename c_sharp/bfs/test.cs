using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace BreadthFirstSearch {

  [TestFixture]
  public class BreadthFirstSearchSpec {

    [Test]
    public void EmptyQueue() {
      var a = new Queue();
      Assert.IsTrue(a.isEmpty());
    }

    [Test]
    public void EnDeQueue() {
      var a = new Queue();

      var n1 = new Node();
      var n2 = new Node();
      var n3 = new Node();
      a.enqueue(n1);
      a.enqueue(n2);
      a.enqueue(n3);
      Assert.IsFalse(a.isEmpty());

      var q1 = a.dequeue();
      var q2 = a.dequeue();
      var q3 = a.dequeue();
      Assert.AreEqual(n1, q1);
      Assert.AreEqual(n2, q2);
      Assert.AreEqual(n3, q3);
    }

    [Test]
    public void DFS() {
      var n1 = new Node(1);
      var n2 = new Node(2);
      var n3 = new Node(3);
      var n4 = new Node(4);
      var n5 = new Node(5);
      var n6 = new Node(6);
      var n7 = new Node(7);
      var n8 = new Node(8);

      n1.adj.Add(n2);
      n1.adj.Add(n3);

      n3.adj.Add(n4);
      n3.adj.Add(n6);

      n4.adj.Add(n8);
      n4.adj.Add(n5);

      n6.adj.Add(n5);
      n6.adj.Add(n7);

      var g = new Graph(n1);

      g.bfs(n1);

      Assert.AreEqual(n1.d, 0);
      Assert.AreEqual(n1.p, null);

      Assert.AreEqual(n2.d, 1);
      Assert.AreEqual(n2.p, n1);

      Assert.AreEqual(n3.d, 1);
      Assert.AreEqual(n3.p, n1);

      Assert.AreEqual(n4.d, 2);
      Assert.AreEqual(n4.p, n3);

      Assert.AreEqual(n5.d, 3);
      Assert.AreEqual(n5.p, n4); // 4 and not 6 because 4 was added before 6.

      Assert.AreEqual(n6.d, 2);
      Assert.AreEqual(n6.p, n3);

      Assert.AreEqual(n7.d, 3);
      Assert.AreEqual(n7.p, n6);

      Assert.AreEqual(n8.d, 3);
      Assert.AreEqual(n8.p, n4);

      foreach(Node n in g.vertices) {
        Assert.AreEqual(n.color, "black");
      }
    }

  }

}