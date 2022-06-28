using System;
using System.Collections.Generic;

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

}