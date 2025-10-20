CREATE TABLE sync_tables
(
    site_id character varying(40) NOT NULL,
    type_id numeric(1,0) NOT NULL,
    table_no numeric(2,0) NOT NULL,
    table_name character varying NOT NULL,
    alive boolean DEFAULT true,
    CONSTRAINT pk_sync_tables PRIMARY KEY (site_id, type_id, table_no)
);


CREATE TABLE setting
(
    site_id  character varying(40) NOT NULL,
    flg_summary_times boolean default false,
    flg_summary_edit boolean default false,
    flg_nighttime boolean default false,
    flg_vessel_movement boolean default false,
    flg_anchorage boolean default false,
    flg_show_rank_category boolean default false,
    flg_show_approval boolean default false,
    work_range numeric(3,1),
    working_hours numeric(3,1),
    deviation numeric(3,1),
    deviation_1week numeric(3,1),
    deviation_4week numeric(3,1),
    deviation_resttime numeric(3,1),
    deviation_long_resttime numeric(3,1),
    deviation_resttime_count numeric(3,1),
    send_flag numeric(1,0),
    data_no numeric(13,0),
    user_key character varying(40) ,
    ts character varying(20) ,
    CONSTRAINT pk_setting PRIMARY KEY (site_id)
);

CREATE TABLE work_content
(
    site_id character varying(40) NOT NULL,
    work_content_id character varying NOT NULL,
    name character varying ,
    dsp_name character varying ,
    bg_color character varying ,
    fg_color character varying ,
    is_include_work_time boolean DEFAULT false,
    is_safety_temporary_labor boolean DEFAULT false,
    show_order integer,
    send_flag numeric(1,0),
    data_no numeric(13,0),
    user_key character varying(40) ,
    ts character varying(20) ,
    CONSTRAINT pk_work_content PRIMARY KEY (site_id, work_content_id)
);

CREATE TABLE role
(
    site_id character varying(40)  NOT NULL,
    role_id character varying(40)  NOT NULL,
    rank_id character varying  NOT NULL,
    ranks character varying  NOT NULL,
    is_approver boolean DEFAULT true,
    alive boolean DEFAULT true,
    send_flag numeric(1,0),
    data_no numeric(13,0),
    user_key character varying(40) ,
    ts character varying(20) ,
    CONSTRAINT pk_role PRIMARY KEY (site_id, role_id)
);

CREATE TABLE rank_category
(
    site_id character varying(40)  NOT NULL,
    rank_category_id character varying(40)  NOT NULL,
    rank_category_name character varying  NOT NULL,
    ranks character varying  NOT NULL,
    show_order integer,
    alive boolean DEFAULT true,
    send_flag numeric(1,0),
    data_no numeric(13,0),
    user_key character varying(40) ,
    ts character varying(20) ,
    CONSTRAINT pk_rank_category PRIMARY KEY (site_id, rank_category_id)
);


CREATE TABLE work
(
    site_id character varying(40)  NOT NULL,
    work_id character varying(40)  NOT NULL,
    squence_date timestamp without time zone NOT NULL,
    crew_no character varying ,
    crew_id character varying ,
    vessel_id character varying ,
    start_work timestamp without time zone NOT NULL,
    start_work_acutual timestamp without time zone NOT NULL,
    finish_work timestamp without time zone,
    finish_work_acutual timestamp without time zone,
    is_deviation boolean DEFAULT false,
    is_deviation_1week boolean DEFAULT false,
    is_deviation_4week boolean DEFAULT false,
    is_deviation_resttime boolean DEFAULT false,
    is_delete boolean DEFAULT false,
    update_date timestamp without time zone,
    alive boolean DEFAULT true,
    work_content_detail character varying ,
    deviation character varying ,
    deviation_1week character varying ,
    deviation_4week character varying ,
    deviation_resttime character varying ,
    night_time character varying ,
    send_flag numeric(1,0),
    data_no numeric(13,0),
    user_key character varying(40) ,
    ts character varying(20) ,
    CONSTRAINT pk_work PRIMARY KEY (site_id, work_id)
);

CREATE TABLE summary_times
(
    site_id character varying(40)  NOT NULL,
    summary_times_id character varying(40)  NOT NULL,
    crew_no character varying ,
    vessel_id character varying ,
    summary_date timestamp without time zone NOT NULL,
    allowance_time character varying ,
    update_date timestamp without time zone NOT NULL,
    alive boolean DEFAULT true,
    send_flag numeric(1,0),
    data_no numeric(13,0),
    user_key character varying(40) ,
    ts character varying(20),
    CONSTRAINT pk_summary_times PRIMARY KEY (site_id, summary_times_id)
);

CREATE TABLE vessel_movement
(
    site_id character varying(40)  NOT NULL,
    vessel_id character varying  NOT NULL,
    date_info character varying  NOT NULL,
    target_date timestamp without time zone NOT NULL,
    full_anchorage boolean DEFAULT false,
    squence_date timestamp without time zone NOT NULL,
    generator_id character varying ,
    movement_info_p1 character varying ,
    movement_info_e1 character varying ,
    movement_info_p2 character varying ,
    movement_info_e2 character varying ,
    anchorage_s1 character varying ,
    anchorage_f1 character varying ,
    anchorage_s2 character varying ,
    anchorage_f2 character varying ,
    send_flag numeric(1,0),
    data_no numeric(13,0),
    user_key character varying(40) ,
    ts character varying(20),
    CONSTRAINT pk_vessel_movement PRIMARY KEY (site_id, vessel_id, date_info)
);

CREATE TABLE vessel_approval_day
(
    site_id character varying(40)  NOT NULL,
    vessel_approval_day_id character varying(40)  NOT NULL,
    vessel_id character varying ,
    approval_day timestamp without time zone,
    approver_crew_no character varying ,
    approved_crew_no character varying ,
    is_delete boolean DEFAULT false,
    squence_date timestamp without time zone NOT NULL,
    alive boolean DEFAULT true,
    send_flag numeric(1,0),
    data_no numeric(13,0),
    user_key character varying(40) ,
    ts character varying(20) ,
    CONSTRAINT pk_vessel_approval_day PRIMARY KEY (site_id, vessel_approval_day_id)
);

CREATE TABLE vessel_approval_month
(
    site_id character varying(40)  NOT NULL,
    vessel_approval_month_id character varying(40)  NOT NULL,
    vessel_id character varying ,
    approval_month timestamp without time zone,
    approver_crew_no character varying ,
    is_delete boolean DEFAULT false,
    squence_date timestamp without time zone NOT NULL,
    alive boolean DEFAULT true,
    send_flag numeric(1,0),
    data_no numeric(13,0),
    user_key character varying(40) ,
    ts character varying(20) ,
    CONSTRAINT pk_vessel_approval_month PRIMARY KEY (site_id, vessel_approval_month_id)
);