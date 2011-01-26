﻿using System;
using NUnit.Framework;
using OpenPop.Mime.Decode;

namespace OpenPopUnitTests.Mime.Decode
{
	[TestFixture]
	public class Rfc2822DateTimeTests
	{
		[Test]
		public void RfcExample()
		{
			const string inputDate = "Fri, 21 Nov 1997 09:55:06 -0600";

			// -0600 we need to add 6 hours when in UTC
			DateTime expectedOutput = new DateTime(1997, 11, 21, 15, 55, 06, DateTimeKind.Utc);
			DateTime output = Rfc2822DateTime.StringToDate(inputDate);

			Assert.AreEqual(expectedOutput, output);
		}

		[Test]
		public void RfcExample2()
		{
			const string inputDate = "Tue, 1 Jul 2003 10:52:37 +0200";

			// +0200 we need to substract 2 hours when in UTC
			DateTime expectedOutput = new DateTime(2003, 7, 1, 8, 52, 37, DateTimeKind.Utc);
			DateTime output = Rfc2822DateTime.StringToDate(inputDate);

			Assert.AreEqual(expectedOutput, output);
		}

		[Test]
		public void NestedComments()
		{
			const string inputDate = "Fri, 21 Nov (foo (bar (baz) ) ) 1997 09:55:06 -0600";

			// -0600 we need to add 6 hours when in UTC
			DateTime expectedOutput = new DateTime(1997, 11, 21, 15, 55, 06, DateTimeKind.Utc);
			DateTime output = Rfc2822DateTime.StringToDate(inputDate);

			Assert.AreEqual(expectedOutput, output);
		}

		[Test]
		public void ObsoleteFormat()
		{
			const string inputDate = "21 Nov 97 09:55:06 GMT";

			// GMT = UTC
			DateTime expectedOutput = new DateTime(1997, 11, 21, 09, 55, 06, DateTimeKind.Utc);
			DateTime output = Rfc2822DateTime.StringToDate(inputDate);

			Assert.AreEqual(expectedOutput, output);
		}

		[Test]
		public void ObsoleteFormatWithWhitespaceAndComment()
		{
			const string inputDate = "Fri, 21 Nov 1997 09(comment):   55  :  06 -0600";

			// -0600 we need to add 6 hours when in UTC
			DateTime expectedOutput = new DateTime(1997, 11, 21, 15, 55, 06, DateTimeKind.Utc);
			DateTime output = Rfc2822DateTime.StringToDate(inputDate);

			Assert.AreEqual(expectedOutput, output);
		}

		[Test]
		public void DateWithNoSeconds()
		{
			const string inputDate = "20 Apr 1988 18:10 +0133";

			// +0133 we need to substract 1 hour and 33 minutes when in UTC
			DateTime expectedOutput = new DateTime(1988, 4, 20, 16, 37, 00, DateTimeKind.Utc);
			DateTime output = Rfc2822DateTime.StringToDate(inputDate);

			Assert.AreEqual(expectedOutput, output);
		}

		[Test]
		public void ObsoleteFormatWithLotsOfWhitespaceAndComments()
		{
			const string inputDate = "(comment)  (comment)  20(comment)  \t Apr(comment) 1988(comment) \t18(comment) :(comment)  \t 10(comment)  \t  +0133 (comment)  \t";

			// +0133 we need to substract 1 hour and 33 minutes when in UTC
			DateTime expectedOutput = new DateTime(1988, 4, 20, 16, 37, 00, DateTimeKind.Utc);
			DateTime output = Rfc2822DateTime.StringToDate(inputDate);

			Assert.AreEqual(expectedOutput, output);
		}

		[Test]
		public void ThrowsIfWrongDayNameUsed()
		{
			const string inputDate = "Tue, 20 Apr 1988 18:10+0133";

			Assert.Throws<ArgumentException>(delegate { Rfc2822DateTime.StringToDate(inputDate); });
		}

		[Test]
		public void ThrowsIfWrongDayNameUsedWithWhitespace()
		{
			const string inputDate = " (foo)Tue, 20 Apr 1988 18:10+0133";

			Assert.Throws<ArgumentException>(delegate { Rfc2822DateTime.StringToDate(inputDate); });
		}

		[Test]
		public void OnlineExample()
		{
			const string inputDate = "Wed, 9 May 2007 12:39:13 -0500 (CDT)";

			DateTime expectedOutput = new DateTime(2007, 5, 9, 17, 39, 13, DateTimeKind.Utc);
			DateTime output = Rfc2822DateTime.StringToDate(inputDate);

			Assert.AreEqual(expectedOutput, output);
		}

		[Test]
		public void MilitaryTimeA()
		{
			const string inputDate = "Wed, 9 May 2007 12:39:13 A";

			// A is equivalent to +0100
			const string inputDateEquivalent = "Wed, 9 May 2007 12:39:13 +0100";

			DateTime inputDateTime = Rfc2822DateTime.StringToDate(inputDate);
			DateTime inputDateTimeEquivalent = Rfc2822DateTime.StringToDate(inputDateEquivalent);

			Assert.AreEqual(inputDateTime, inputDateTimeEquivalent);
		}

		[Test]
		public void MilitaryTimeC()
		{
			const string inputDate = "Wed, 9 May 2007 12:39:13 C";

			// C is equivalent to +0300
			const string inputDateEquivalent = "Wed, 9 May 2007 12:39:13 +0300";

			DateTime inputDateTime = Rfc2822DateTime.StringToDate(inputDate);
			DateTime inputDateTimeEquivalent = Rfc2822DateTime.StringToDate(inputDateEquivalent);

			Assert.AreEqual(inputDateTime, inputDateTimeEquivalent);
		}

		[Test]
		public void MilitaryTimeJ()
		{
			const string inputDate = "Wed, 9 May 2007 12:39:13 J";

			// J is not a military time
			Assert.Throws<ArgumentException>(delegate { Rfc2822DateTime.StringToDate(inputDate); });
		}

		[Test]
		public void MilitaryTimeL()
		{
			const string inputDate = "Wed, 9 May 2007 12:39:13 L";

			// L is equivalent to +1100
			const string inputDateEquivalent = "Wed, 9 May 2007 12:39:13 +1100";

			DateTime inputDateTime = Rfc2822DateTime.StringToDate(inputDate);
			DateTime inputDateTimeEquivalent = Rfc2822DateTime.StringToDate(inputDateEquivalent);

			Assert.AreEqual(inputDateTime, inputDateTimeEquivalent);
		}

		[Test]
		public void MilitaryTimeO()
		{
			const string inputDate = "Wed, 9 May 2007 12:39:13 O";

			// O is equivalent to -0200
			const string inputDateEquivalent = "Wed, 9 May 2007 12:39:13 -0200";

			DateTime inputDateTime = Rfc2822DateTime.StringToDate(inputDate);
			DateTime inputDateTimeEquivalent = Rfc2822DateTime.StringToDate(inputDateEquivalent);

			Assert.AreEqual(inputDateTime, inputDateTimeEquivalent);
		}

		[Test]
		public void MilitaryTimeY()
		{
			const string inputDate = "Wed, 9 May 2007 12:39:13 Y";

			// Y is equivalent to -1200
			const string inputDateEquivalent = "Wed, 9 May 2007 12:39:13 -1200";

			DateTime inputDateTime = Rfc2822DateTime.StringToDate(inputDate);
			DateTime inputDateTimeEquivalent = Rfc2822DateTime.StringToDate(inputDateEquivalent);

			Assert.AreEqual(inputDateTime, inputDateTimeEquivalent);
		}

		[Test]
		public void MilitaryTimeZ()
		{
			const string inputDate = "Wed, 9 May 2007 12:39:13 Z";

			// Z is equivalent to +0000
			const string inputDateEquivalent = "Wed, 9 May 2007 12:39:13 +0000";

			DateTime inputDateTime = Rfc2822DateTime.StringToDate(inputDate);
			DateTime inputDateTimeEquivalent = Rfc2822DateTime.StringToDate(inputDateEquivalent);

			Assert.AreEqual(inputDateTime, inputDateTimeEquivalent);
		}

		[Test]
		public void UsTimeZoneEdt()
		{
			const string inputDate = "Wed, 9 May 2007 12:39:13 EDT";

			// EDT is equivalent to -0400
			const string inputDateEquivalent = "Wed, 9 May 2007 12:39:13 -0400";

			DateTime inputDateTime = Rfc2822DateTime.StringToDate(inputDate);
			DateTime inputDateTimeEquivalent = Rfc2822DateTime.StringToDate(inputDateEquivalent);

			Assert.AreEqual(inputDateTime, inputDateTimeEquivalent);
		}

		[Test]
		public void UsTimeZoneGmt()
		{
			const string inputDate = "Wed, 9 May 2007 12:39:13 GMT";

			// GMT is equivalent to +0000
			const string inputDateEquivalent = "Wed, 9 May 2007 12:39:13 +0000";

			DateTime inputDateTime = Rfc2822DateTime.StringToDate(inputDate);
			DateTime inputDateTimeEquivalent = Rfc2822DateTime.StringToDate(inputDateEquivalent);

			Assert.AreEqual(inputDateTime, inputDateTimeEquivalent);
		}

		[Test]
		public void UsTimeZoneUt()
		{
			const string inputDate = "Wed, 9 May 2007 12:39:13 UT";

			// UT is equivalent to +0000
			const string inputDateEquivalent = "Wed, 9 May 2007 12:39:13 +0000";

			DateTime inputDateTime = Rfc2822DateTime.StringToDate(inputDate);
			DateTime inputDateTimeEquivalent = Rfc2822DateTime.StringToDate(inputDateEquivalent);

			Assert.AreEqual(inputDateTime, inputDateTimeEquivalent);
		}

		[Test]
		public void DateWithNoSpace()
		{
			// This is actually an illigal string
			// But such a string was met, and therefore a robust parser should
			// be able to parse it
			const string inputDate = "19 Jan 2011 13:24:54+0000";

			DateTime expectedOutput = new DateTime(2011, 1, 19, 13, 24, 54, DateTimeKind.Utc);
			DateTime output = Rfc2822DateTime.StringToDate(inputDate);

			Assert.AreEqual(expectedOutput, output);
		}
	}
}