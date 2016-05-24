namespace NeosIT.Exchange.GenericExchangeTransportAgent.Plugins.Common.Impl.Utils
{
    using System;
    using System.IO;

    using MimeDetective;

    public class Mime
    {
        private static readonly int MaxHeaderLength = 560;

        public static string GetMimeFromStream(Stream stream)
        {
            if (null == stream)
            {
                throw new ArgumentNullException("stream");
            }

            long originalPosition = 0;

            if (stream.CanSeek)
            {
                originalPosition = stream.Position;
                stream.Position = 0;
            }

            try
            {
                var bufferSize = stream.Length > MaxHeaderLength ? MaxHeaderLength : stream.Length;
                var buffer = new byte[bufferSize];
                stream.Read(buffer, 0, MaxHeaderLength);
                var fileType = MimeTypes.GetFileType(buffer);
                return fileType.Mime;
            }
            catch (Exception)
            {
                return "unknown/unknown";
            }
            finally
            {
                if (stream.CanSeek)
                {
                    stream.Position = originalPosition;
                }
            }
        }
    }
}