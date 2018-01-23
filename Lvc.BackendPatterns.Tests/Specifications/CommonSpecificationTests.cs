using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Lvc.BackendPatterns.Tests.Specifications
{
    public abstract class CommonSpecificationTests
    {
        #region IsSatisfiedBy

        [Theory, MemberData(
            nameof(CommonSpecificationTests_Data.IsSatisfiedBy_Int),
            MemberType = typeof(CommonSpecificationTests_Data))]
        public void IsSatisfiedBy_Int(int t, bool expectedResult)
        {
            // Arrange

            // Act

            // Assert

        }

        [Theory, MemberData(
            nameof(CommonSpecificationTests_Data.IsSatisfiedBy_String),
            MemberType = typeof(CommonSpecificationTests_Data))]
        public void IsSatisfiedBy_String(string t, bool expectedResult)
        {
            // Arrange

            // Act

            // Assert

        }

        #endregion IsSatisfiedBy
    }
}
