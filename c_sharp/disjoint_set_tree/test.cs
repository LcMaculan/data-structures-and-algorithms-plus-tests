using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace DisjointSetTree {

  [TestFixture]
  public class DisjointSetTreeSpec {

    [Test]
    public void EmptyDisjointSetTree() {
      var a = new DisjointSetTree();

      Assert.AreEqual(a.p, a);
      Assert.AreEqual(a.value, null);
      Assert.AreEqual(a.rank, 0);
    }

    [Test]
    public void SingleElement() {
      DisjointSetTree a = new DisjointSetTree(1);
      Assert.AreEqual(a.p, a);
      Assert.AreEqual(a.value, 1);
      Assert.AreEqual(a.rank, 0);
    }

    [Test]
    public void SingleUnion() {
      DisjointSetTree a = new DisjointSetTree(1);
      DisjointSetTree b = new DisjointSetTree(2);

      Assert.AreNotEqual(a.find(), b.find());

      var u = a.union(b);
      Assert.AreEqual(a.find(), b.find());
      Assert.AreEqual(u.find().rank, 1);
    }


    [Test]
    public void UnionOfUnions() {
      DisjointSetTree a = new DisjointSetTree(1);
      DisjointSetTree b = new DisjointSetTree(2);
      DisjointSetTree c = new DisjointSetTree(3);
      DisjointSetTree d = new DisjointSetTree(4);
      DisjointSetTree e = new DisjointSetTree(5);

      Assert.AreNotEqual(a.find(), b.find());
      Assert.AreNotEqual(a.find(), e.find());

      var u1 = a.union(b);
      Assert.AreEqual(a.find(), b.find());
      //
      Assert.AreEqual(u1.find(), a.find());
      Assert.AreEqual(u1.find(), b.find());

      var u2 = e.union(d).union(c);
      Assert.AreEqual(e.find(), d.find());
      Assert.AreEqual(d.find(), c.find());
      Assert.AreEqual(c.find(), e.find());
      //
      Assert.AreEqual(u2.find(), d.find());
      Assert.AreEqual(u2.find(), c.find());
      Assert.AreEqual(u2.find(), e.find());

      var u3 = u2.union(u1);
      Assert.AreEqual(u3.find(), u2.find());
      Assert.AreEqual(u3.find(), u1.find());
    }

  }

}