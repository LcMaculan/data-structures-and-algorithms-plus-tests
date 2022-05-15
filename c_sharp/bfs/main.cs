using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace BreadthFirstSearch {
  public class Queue {
    public List<Node> queue;

    public Queue() {
      this.queue = new List<Node>();
    }

    public bool isEmpty() {
      return this.queue.Count == 0;
    }

    public void enqueue(Node node) {
      this.queue.Add(node);
    }

    public Node dequeue() {
      var result = this.queue[0];
      this.queue.RemoveAt(0);
      return result;
    }

    public void empty() {
      this.queue.Clear();
    }

  }

  public class Node {
    public List<Node> adj;
    public String color;
    public int? d;
    public Node p;
    public int? value;

    public Node() {
      this.adj = new List<Node>();
      this.color = null;
      this.d = null;
      this.p = null;
      this.value = null;
    }

    public Node(int value) : this() {
      this.value = value;
    }
  }

  public class Graph {
    public List<Node> vertices;
    private Queue queue;

    // edges
    public Graph() {
      this.vertices = new List<Node>();
      this.queue = new Queue();
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

    public void bfs(Node node) {
      foreach (Node n in this.vertices) {
        n.color = "white";
        n.d = int.MaxValue;
        n.p = null;
      }
      node.color = "gray";
      node.d = 0;
      this.queue.empty();
      this.queue.enqueue(node);
      while (!this.queue.isEmpty()) {
        var n = this.queue.dequeue();
        foreach(Node m in n.adj) {
          if (m.color == "white") {
            m.p = n;
            m.color = "gray";
            m.d = n.d + 1;
            this.queue.enqueue(m);
          }
        }
        n.color = "black";
      }
    }

  }

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