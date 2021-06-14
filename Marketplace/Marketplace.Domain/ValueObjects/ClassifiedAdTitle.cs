using Marketplace.Framework.Helpers;
using System;
using System.Text.RegularExpressions;

namespace Marketplace.Domain.ValueObjects
{
    public class ClassifiedAdTitle : Value<ClassifiedAdTitle>
    {
        #region Fields

        private const int ValueMaxLength = 100;

        private readonly string _value;

        #endregion

        #region Initializers

        internal ClassifiedAdTitle(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException($"'{nameof(value)}' cannot be null or whitespace.", nameof(value));
            }

            _value = value;
        }

        #endregion

        #region Public Methods

        public static ClassifiedAdTitle FromString(string title)
        {
            CheckValidity(title);

            return new(title);
        }

        public static ClassifiedAdTitle FromHtml(string htmlTitle)
        {
            var italicTag = (Html: (Open: "<i>", Close: "</i>"), Markdown: "*");
            var boldTag = (Html: (Open: "<b>", Close: "</b>"), Markdown: "**");

            var supportedTagsReplaced = htmlTitle
                .Replace(italicTag.Html.Open, italicTag.Markdown)
                .Replace(italicTag.Html.Close, italicTag.Markdown)
                .Replace(boldTag.Html.Open, boldTag.Markdown)
                .Replace(boldTag.Html.Close, boldTag.Markdown);

            var title = Regex.Replace(supportedTagsReplaced, "<.*?>", string.Empty);

            CheckValidity(title);

            return new(title);
        }

        public static implicit operator string(ClassifiedAdTitle classifiedAdTitle)
        {
            return classifiedAdTitle._value;
        }

        public override bool Equals(ClassifiedAdTitle other)
        {
            if (other is null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return _value.Equals(other._value);
        }

        public override int HashCode()
        {
            return _value.GetHashCode();
        }

        #endregion

        #region Private Methods

        private static void CheckValidity(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException($"'{nameof(value)}' cannot be null or whitespace.", nameof(value));
            }

            if (value.Length > ValueMaxLength)
            {
                throw new ArgumentOutOfRangeException(nameof(value),
                    $"Title cannot be longer than {ValueMaxLength} characters");
            }
        }

        #endregion
    }
}