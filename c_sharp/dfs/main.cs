using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace DepthFirstSearch {
  public class Node {
    public List<Node> adj;
    public String color;

    public Node p;
    public int? value;
    public int? d;
    public int? f;

    public Node() {
      this.adj = new List<Node>();
      this.color = null;
      this.p = null;
      this.value = null;
      this.d = null;
      this.f = null;
    }

    public Node(int value) : this() {
      this.value = value;
    }
  }

  public class Graph {
    public List<Node> vertices;
    private int time;

    // edges
    public Graph() {
      this.vertices = new List<Node>();
      this.time = 0;
    }

    public Graph(Node node) : this() {
      Graph.initalizeVertices(this, node);
    }

    public static void initalizeVertices(Graph graph, Node node) {
      graph.vertices.Add(node);
      foreach(Node n in node.adj) {
        Graph.initalizeVertices(graph, n);
      }
    }

    private void dfsVisit(Node node) {
      node.color = "gray";
      this.time += 1;
      node.d = this.time;

      foreach (Node n in node.adj) {
        if (n.color == "white") {
          n.p = node;
          this.dfsVisit(n);
        }
      }
      node.color = "black";
      this.time += 1;
      node.f = this.time;
    }

    public void dfs() {
      foreach (Node n in this.vertices) {
        n.color = "white";
        n.p = null;
      }
      this.time = 0;

      foreach (Node n in this.vertices) {
        if (n.color == "white") {
          this.dfsVisit(n);
        }
      }
    }

  }

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