namespace PrimusFlex.Web.Infrastructure
{
    public static class Constant
    {
        public const string WEB_SITE_NAME = "Primus Flex Ltd.";

        // Roles
        public const string ROLE_SUPPER_ADMIN = "SupperAdmin";
        public const string ROLE_ADMIN = "Admin";
        public const string ROLE_MANAGER = "Manager";
        public const string ROLE_WORKER = "Worker";

        // Storage
        public const string PRIMARY_BLOB_SERVICE_ENDPOINT = "https://primusflex.blob.core.windows.net/";
        public const string IMAGES_CONTAINER = "image-container";

        // Tables
        public const int NUMBER_OF_IMAGES_IN_ROW = 6;
    }
}
