using System;
using System.Collections.Generic;
using System.Text;

namespace DataIngestion.TestAssignment.Infrastructure
{
    public static class GlobalConstants
    {
        public static readonly string ArtistFileId = "1wwzvIidkUHaO3slP4gP9QJbrhXV2N_vl";
        public static readonly string ArtistCollectionFileId = "1w7C56VpogAIVvzOY1m5b-LqZCHcttq8d";
        public static readonly string CollectionFileId = "1AJ7icRJ5dfbWlQORocfrLhVyMOd242sm";
        public static readonly string CollectionMatchFileId = "1EvNWIsRNL-YIk8_oVq7AmqRwjGGnaycr";
        public static readonly string GoogleDriveDirectDownloadUrlRoot = "https://drive.google.com/uc?export=download&id=";
        public static readonly string GoogleDriveDirectDownloadConfirmationUrlRoot = "https://drive.google.com/uc?export=download&confirm=";
        public static readonly string GoogleDriveDirectDownloadConfirmationUrlPart2 = "&id=";
        public static readonly string ElasticSearchUrlRoot = "https://match.collection.com/uc?export=download&downloadUrl=";
    }
}
