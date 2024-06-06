//--------------------------
// TARTEEB LLC               
// ALL RIGHTS RESERVED      
//--------------------------

using MasterStream.Core.API.Models.Exceptions;
using MasterStream.Core.API.Models.VideoMetadatas;

namespace MasterStream.Core.API.Services.VideoMetadatas
{
    public partial class VideoMetadataService
    {
        private void ValidationVideoMetadataOnAdd(VideoMetadata videoMetadata)
        {
            ValidateVideoMetadata(videoMetadata);

            Validate(
                (Rule: IsInvalid(videoMetadata.Id), Parameter: nameof(VideoMetadata.Id)),
                (Rule: IsInvalid(videoMetadata.Title), Parameter: nameof(VideoMetadata.Title)),
                (Rule: IsInvalid(videoMetadata.BlobPath), Parameter: nameof(VideoMetadata.BlobPath)),
                (Rule: IsInvalid(videoMetadata.CreatedAt), Parameter: nameof(VideoMetadata.CreatedAt)),
                (Rule: IsInvalid(videoMetadata.UpdatedDate), Parameter: nameof(VideoMetadata.UpdatedDate))
                );
        }
        private void ValidateVideoMetadata(VideoMetadata videoMetadata)
        {
            if (videoMetadata is null)
            {
                throw new NullVideoMetadataException(
                    message: "Video metadata is null");
            }
        }

        private dynamic IsInvalid(Guid id) => new
        {
            Condition = id == Guid.Empty,
            Message = "Id is required"
        };

        private dynamic IsInvalid(string text) => new
        {
            Condition = String.IsNullOrWhiteSpace(text),
            Message = "Text is required"
        };

        private dynamic IsInvalid(DateTimeOffset date) => new
        {
            Condition = date == default,
            Message = "Date is required"
        };

        //Validations engine
        private void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidVideoMetadataException = new InvalidVideoMetadataException(
                    message: "Video metadata is invalid");

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidVideoMetadataException.UpsertDataList(
                        parameter,
                        value: rule.Message);
                }
            }

            invalidVideoMetadataException.ThrowIfContainsErrors();
        }
    }
}
