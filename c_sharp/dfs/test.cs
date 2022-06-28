using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace DepthFirstSearch {

  [TestFixture]
  public class DepthFirstSearchSpec {

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

      g.dfs();

      Assert.AreEqual(n1.p, null);
      Assert.AreEqual(n2.p, n1);
      Assert.AreEqual(n3.p, n1);
      Assert.AreEqual(n4.p, n3);
      Assert.AreEqual(n5.p, n4);
      Assert.AreEqual(n6.p, n3);
      Assert.AreEqual(n7.p, n6);
      Assert.AreEqual(n8.p, n4);

      foreach(Node n in g.vertices) {
        Assert.AreEqual(n.color, "black");
      }
    }

  }

}