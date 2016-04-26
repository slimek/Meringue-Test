using System.Collections.Generic;
using NUnit.Framework;
using Meringue;

#pragma warning disable 1718  // x == x


using CiString = Meringue.CaseInsensitiveString;

public class CaseInsensitiveSuite
{
    [Test]
    public void CaseInsensitiveStringTest()
    {
        var empty = new CiString();
        var alice = new CiString( "Alice" );
        var lower = new CiString( "alice" );
        var upper = new CiString( "ALICE" );
        var camel = new CiString( "Alice" );
        var reimu = new CiString( "Reimu" );


        // Initial Values //

        Assert.AreEqual( "", empty.value );
        Assert.AreEqual( "Alice", alice.value );
        Assert.AreEqual( "alice", lower.value );
        Assert.AreEqual( "ALICE", upper.value );


        // Equality //

        // X == X
        Assert.True( alice.Equals( alice ));
        Assert.True( alice == alice );
        Assert.False( alice != alice );
        Assert.True( alice.GetHashCode() == alice.GetHashCode() );

        // X == Y
        Assert.True( alice == camel );
        Assert.True( alice != reimu );
        Assert.True( alice.GetHashCode() == camel.GetHashCode() );

        // X == Y, Case Insensitive
        Assert.True( alice == lower );
        Assert.True( alice == upper );
        Assert.True( lower == upper );
        Assert.True( lower.GetHashCode() == upper.GetHashCode() );
        Assert.AreNotEqual( lower.value, upper.value );

        // X == ref of X
        var reffer = alice;
        Assert.True( reffer.Equals( alice ));
        Assert.True( reffer == alice );
        Assert.True( reffer.GetHashCode() == alice.GetHashCode() );

        // X and null
        Assert.False( alice.Equals( null ));
        Assert.False( alice == null );
        Assert.False( null == alice );
        CiString nullA = null;
        CiString nullB = null;
        Assert.True( nullA == nullB );

        // X and other type
        // - Never equals to another type.
        //   But you may compare its value.
        Assert.False( alice.Equals( "Alice" ));
        Assert.True( alice.value == "Alice" );


        // Comparison //

        // Compare
        Assert.True( alice.CompareTo( alice ) == 0 );
        Assert.True( alice.CompareTo( reimu ) == -1 );
        Assert.True( CiString.Compare( alice, alice ) == 0 );
        Assert.True( CiString.Compare( reimu, alice ) == 1 );

        // Compare to null
        Assert.True( alice.CompareTo( null ) == 1 );
        Assert.True( CiString.Compare( alice, null ) == 1 );
        Assert.True( CiString.Compare( null, alice ) == -1 );
        Assert.True( CiString.Compare( null, null ) == 0 );

        // Compare to string
        // - This is not allowed. CaseInsensitive type should be strong-typed.
        //   Buy you may compare its value to a string.
        Assert.Throws< System.ArgumentException >( () => { alice.CompareTo( "Yukari" ); } );

        // X op X
        Assert.False( alice < alice );
        Assert.False( alice > alice );
        Assert.True( alice <= alice );
        Assert.True( alice >= alice );

        // X op Y
        Assert.True( alice < reimu );
        Assert.True( alice <= reimu );
        Assert.False( alice > reimu );
        Assert.False( alice >= reimu );

        // X op Y, case insensitive
        Assert.False( alice < upper );
        Assert.False( alice > upper );
        Assert.False( alice < lower );
        Assert.False( alice > lower );


        // Placed in Hashing Collection //

        var set = new HashSet< CiString >();

        Assert.True( set.Add( alice ));
        Assert.True( set.Add( reimu ));

        // Cannot add "duplcate" values because their are case insensitive.
        Assert.False( set.Add( lower ));
        Assert.False( set.Add( upper ));

        // Find
        Assert.True( set.Contains( alice ));
        Assert.True( set.Contains( reimu ));
        Assert.False( set.Contains( empty ));

        // Find, case insensitive
        Assert.True( set.Contains( lower ));
        Assert.True( set.Contains( upper ));


        // Sorting //

        var list = new List< CiString >();

        list.Add( reimu );
        list.Add( empty );
        list.Add( alice );

        list.Sort();

        Assert.True( list[0] == empty );
        Assert.True( list[1] == alice );
        Assert.True( list[2] == reimu );


        // Formatting //

        Assert.AreEqual( "Name is Alice", string.Format( "Name is {0}", alice ));
    }

#if CANNOT_COMPILE
    void CaseInsensitiveCannotCompileTest()
    {
        var langId = LanguageId.Chinese;
        var citext = new CiString( "citext" );

        // You cannot compare two different case insensitive types
        var flag1 = ( langId == citext );
        var flag2 = ( langId > citext );
        var flag3 = CiString.Compare( langId, citext );
    }


#endif  // CANNOT_COMPILE

}
