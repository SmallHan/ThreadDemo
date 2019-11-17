namespace ReptileZhiHu
{

    public class ZhJsonEntity
    {
        public Ad_Info ad_info { get; set; }
        public Datum[] data { get; set; }
        public Paging paging { get; set; }
    }

    public class Ad_Info
    {
        public Ad ad { get; set; }
        public string adjson { get; set; }
        public int position { get; set; }
    }

    public class Ad
    {
        public string ad_verb { get; set; }
        public Brand brand { get; set; }
        public int category { get; set; }
        public string[] click_tracks { get; set; }
        public string close_track { get; set; }
        public string[] close_tracks { get; set; }
        public string[] conversion_tracks { get; set; }
        public Creative[] creatives { get; set; }
        public string[] debug_tracks { get; set; }
        public bool display_advertising_tag { get; set; }
        public string experiment_info { get; set; }
        public int id { get; set; }
        public string[] impression_tracks { get; set; }
        public bool is_evergreen { get; set; }
        public bool is_new_webview { get; set; }
        public bool is_speeding { get; set; }
        public bool is_webp { get; set; }
        public bool land_prefetch { get; set; }
        public string name { get; set; }
        public bool native_prefetch { get; set; }
        public int party_id { get; set; }
        public string revert_close_track { get; set; }
        public string template { get; set; }
        public string view_track { get; set; }
        public string[] view_tracks { get; set; }
        public string za_ad_info { get; set; }
        public string za_ad_info_json { get; set; }
    }

    public class Brand
    {
        public int id { get; set; }
        public string logo { get; set; }
        public string name { get; set; }
    }

    public class Creative
    {
        public string app_promotion_url { get; set; }
        public Brand1 brand { get; set; }
        public Cta cta { get; set; }
        public string description { get; set; }
        public Footer footer { get; set; }
        public string landing_url { get; set; }
        public string title { get; set; }
    }

    public class Brand1
    {
        public int id { get; set; }
        public string logo { get; set; }
        public string name { get; set; }
    }

    public class Cta
    {
        public string value { get; set; }
    }

    public class Footer
    {
        public string value { get; set; }
    }

    public class Paging
    {
        public bool is_end { get; set; }
        public bool is_start { get; set; }
        public string next { get; set; }
        public string previous { get; set; }
        public int totals { get; set; }
    }

    public class Datum
    {
        public int id { get; set; }
        public string type { get; set; }
        public string answer_type { get; set; }
        public Question question { get; set; }
        public Author author { get; set; }
        public string url { get; set; }
        public bool is_collapsed { get; set; }
        public int created_time { get; set; }
        public int updated_time { get; set; }
        public string extras { get; set; }
        public bool is_copyable { get; set; }
        public bool is_normal { get; set; }
        public int voteup_count { get; set; }
        public int comment_count { get; set; }
        public bool is_sticky { get; set; }
        public bool admin_closed_comment { get; set; }
        public string comment_permission { get; set; }
        public Can_Comment can_comment { get; set; }
        public string reshipment_settings { get; set; }
        public string content { get; set; }
        public string editable_content { get; set; }
        public string excerpt { get; set; }
        public string collapsed_by { get; set; }
        public string collapse_reason { get; set; }
        public object annotation_action { get; set; }
        public Relevant_Info relevant_info { get; set; }
        public Suggest_Edit suggest_edit { get; set; }
        public bool is_labeled { get; set; }
        public Reward_Info reward_info { get; set; }
        public Relationship1 relationship { get; set; }
    }

    public class Question
    {
        public string type { get; set; }
        public int id { get; set; }
        public string title { get; set; }
        public string question_type { get; set; }
        public int created { get; set; }
        public int updated_time { get; set; }
        public string url { get; set; }
        public Relationship relationship { get; set; }
    }

    public class Relationship
    {
    }

    public class Author
    {
        public string id { get; set; }
        public string url_token { get; set; }
        public string name { get; set; }
        public string avatar_url { get; set; }
        public string avatar_url_template { get; set; }
        public bool is_org { get; set; }
        public string type { get; set; }
        public string url { get; set; }
        public string user_type { get; set; }
        public string headline { get; set; }
        public Badge[] badge { get; set; }
        public int gender { get; set; }
        public bool is_advertiser { get; set; }
        public bool is_followed { get; set; }
        public bool is_privacy { get; set; }
        public int follower_count { get; set; }
        public bool is_following { get; set; }
        public bool is_celebrity { get; set; }
        public bool is_blocking { get; set; }
        public bool is_blocked { get; set; }
    }

    public class Badge
    {
        public string type { get; set; }
        public string description { get; set; }
        public object[] topics { get; set; }
    }

    public class Can_Comment
    {
        public string reason { get; set; }
        public bool status { get; set; }
    }

    public class Relevant_Info
    {
        public bool is_relevant { get; set; }
        public string relevant_type { get; set; }
        public string relevant_text { get; set; }
    }

    public class Suggest_Edit
    {
        public string reason { get; set; }
        public bool status { get; set; }
        public string tip { get; set; }
        public string title { get; set; }
        public Unnormal_Details unnormal_details { get; set; }
        public string url { get; set; }
    }

    public class Unnormal_Details
    {
        public string status { get; set; }
        public string description { get; set; }
        public string reason { get; set; }
        public int reason_id { get; set; }
        public string note { get; set; }
    }

    public class Reward_Info
    {
        public bool can_open_reward { get; set; }
        public bool is_rewardable { get; set; }
        public int reward_member_count { get; set; }
        public int reward_total_money { get; set; }
        public string tagline { get; set; }
    }

    public class Relationship1
    {
        public bool is_author { get; set; }
        public bool is_authorized { get; set; }
        public bool is_nothelp { get; set; }
        public bool is_thanked { get; set; }
        public bool is_recognized { get; set; }
        public int voting { get; set; }
        public object[] upvoted_followees { get; set; }
    }

}
