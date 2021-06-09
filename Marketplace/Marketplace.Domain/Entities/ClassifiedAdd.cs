using Marketplace.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Domain.Entities
{
    public class ClassifiedAdd
    {
        #region Fields

        private UserId _ownerId;
        private string _title;
        private string _text;
        private decimal _price;

        #endregion

        #region Properties

        public ClassifiedAddId Id { get; }

        #endregion

        #region Initializers

        public ClassifiedAdd(ClassifiedAddId id, UserId ownerId)
        {            
            Id = id ?? throw new ArgumentNullException(nameof(id));
            _ownerId = ownerId ?? throw new ArgumentNullException(nameof(ownerId));
        }

        #endregion

        #region Public Methods

        public void SetTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException($"'{nameof(title)}' cannot be null or whitespace.", nameof(title));
            }

            _title = title;
        }

        public void UpdateText(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentException($"'{nameof(text)}' cannot be null or whitespace.", nameof(text));
            }

            _text = text;
        }

        public void UpdatePrice(decimal price)
        {
            _price = price;
        }

        #endregion

        #region Private Methods



        #endregion
    }
}
