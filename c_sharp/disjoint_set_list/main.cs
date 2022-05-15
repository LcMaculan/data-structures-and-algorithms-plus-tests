using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace DisjointSetList {

  public class DisjointSetList {
    public DisjointSetList next;
    public DisjointSetList rap;
    public DisjointSetList last;
    public int length;
    public int? value;

    public DisjointSetList() {
      this.next = null;
      this.rap = this;
      this.last = this;
 
      this.value = null;
      this.length = 0;
    }

    public DisjointSetList(int value) : this() {
      this.value = value;
      this.length = 1;
    }

    public DisjointSetList find() {
      return this.rap;
    }

    private static void link(DisjointSetList x, DisjointSetList y) {
      var j = x.last;
      j.next = y;
      x.last = y.last;
      x.length += y.length;
      while (y != null) {
        y.rap = x.rap;
        y = y.next;
      }
    }

    public DisjointSetList union(DisjointSetList disjointSetList) {
      if (this.find() != disjointSetList.find()) {
        if (this.length > disjointSetList.length) {
          DisjointSetList.link(this, disjointSetList);
          return this;
        }
        else {
          DisjointSetList.link(disjointSetList, this);
          return disjointSetList;
        }
      }
      return this;
    }

    public List<int?> getValues() {
      var result = new List<int?>();
      var currentNode = this.find();

      while (currentNode != null) {
        result.Add(currentNode.value);
        currentNode = currentNode.next;
      }

      return result;
    }

  }

  [TestFixture]
  public class DisjointSetListSpec {

    [Test]
    public void EmptyDisjointSetList() {
      DisjointSetList a = new DisjointSetList();
      Assert.AreEqual(a.rap, a);
      Assert.AreEqual(a.length, 0);
      Assert.AreEqual(a.last, a);
      Assert.AreEqual(a.value, null);
    }

    [Test]
    public void SingleElement() {
      DisjointSetList a = new DisjointSetList(1);
      Assert.AreEqual(a.rap, a);
      Assert.AreEqual(a.length, 1);
      Assert.AreEqual(a.last, a);
      Assert.AreEqual(a.value, 1);
    }

    [Test]
    public void SingleUnion() {
      DisjointSetList a = new DisjointSetList(1);
      DisjointSetList b = new DisjointSetList(2);

      Assert.AreNotEqual(a.find(), b.find());

      var u = a.union(b);
      Assert.AreEqual(a.find(), b.find());
      Assert.AreEqual(u.find().length, 2);
    }


    [Test]
    public void UnionOfUnions() {
      DisjointSetList a = new DisjointSetList(1);
      DisjointSetList b = new DisjointSetList(2);
      DisjointSetList c = new DisjointSetList(3);
      DisjointSetList d = new DisjointSetList(4);
      DisjointSetList e = new DisjointSetList(5);

      Assert.AreNotEqual(a.find(), b.find());
      Assert.AreNotEqual(a.find(), e.find());

      var u1 = a.union(b);
      Assert.AreEqual(a.find(), b.find());
      //
      Assert.AreEqual(u1.find(), a.find());
      Assert.AreEqual(u1.find(), b.find());
      //
      Assert.IsTrue(u1.getValues().Contains(1));
      Assert.IsTrue(u1.getValues().Contains(2));

      var u2 = e.union(d).union(c);
      Assert.AreEqual(e.find(), d.find());
      Assert.AreEqual(d.find(), c.find());
      Assert.AreEqual(c.find(), e.find());
      //
      Assert.AreEqual(u2.find(), d.find());
      Assert.AreEqual(u2.find(), c.find());
      Assert.AreEqual(u2.find(), e.find());
      //
      Assert.IsTrue(u2.getValues().Contains(3));
      Assert.IsTrue(u2.getValues().Contains(4));
      Assert.IsTrue(u2.getValues().Contains(5));

      var u3 = u2.union(u1);
      Assert.AreEqual(u3.find(), u2.find());
      Assert.AreEqual(u3.find(), u1.find());
      //
      Assert.IsTrue(u3.getValues().Contains(1));
      Assert.IsTrue(u3.getValues().Contains(2));
      Assert.IsTrue(u3.getValues().Contains(3));
      Assert.IsTrue(u3.getValues().Contains(4));
      Assert.IsTrue(u3.getValues().Contains(5));
    }

  }

}