namespace ApplicationName.ModelsAndFacilities.Facilities.Interfaces
{
    public interface IHttpAuthenticationProcedure
    {
        public IHttpAuthParameters GetHttpAuthenticationParameters(IAuthParameters authParameters);
    }
}
