#
#
#
ALTER TABLE MS_USER ALTER COLUMN MAIL_ADDRESS TYPE character varying(50);

ALTER TABLE si_card ADD ms_user_id character varying(40);




ALTER TABLE sn_table_info ALTER COLUMN name TYPE character varying;


CREATE TABLE si_labor_management_record_book
(
    si_labor_management_record_book_id character varying(40) PRIMARY KEY,
    overtime_work_agreement numeric(1,0),
    compensation_holiday_labor_agreement numeric(1,0),
    break_time_division_agreement numeric(1,0),
    standard_working_period numeric(9,0),
    start_standard_working_period timestamp without time zone,
    last_standard_working_period timestamp without time zone,
    delete_flag numeric(1,0) NOT NULL DEFAULT 0,
    user_key character varying(40),
    data_no numeric(13,0),
    send_flag numeric(1,0) NOT NULL DEFAULT 0,
    vessel_id numeric(4,0) NOT NULL,
    renew_user_id character varying(40),
    renew_date timestamp without time zone NOT NULL,
    ts character varying(20)
);

CREATE TABLE si_required_number_of_days
(
    si_required_number_of_days_id character varying(40) PRIMARY KEY,
    kind numeric(1,0),
    ms_senin_company_id character varying(40),
    days numeric(3,0),
    delete_flag numeric(1,0) NOT NULL DEFAULT 0,
    user_key character varying(40),
    data_no numeric(13,0),
    send_flag numeric(1,0) NOT NULL DEFAULT 0,
    vessel_id numeric(4,0) NOT NULL,
    renew_user_id character varying(40) NOT NULL,
    renew_date timestamp without time zone NOT NULL,
    ts character varying(20)
);

CREATE TABLE si_night_setting
(
    si_night_setting_id character varying(40) PRIMARY KEY,
    ms_senin_company_id character varying(40),
    ms_vessel_id numeric(4,0),
    start_time numeric(4,0),
    end_time numeric(4,0),
    delete_flag numeric(1,0) NOT NULL DEFAULT 0,
    user_key character varying(40),
    data_no numeric(13,0),
    send_flag numeric(1,0) NOT NULL DEFAULT 0,
    vessel_id numeric(4,0) NOT NULL,
    renew_user_id character varying(40) NOT NULL,
    renew_date timestamp without time zone NOT NULL,
    ts character varying(20)
);



ALTER TABLE sn_parameter ADD maintenance_flag numeric(1,0);
ALTER TABLE sn_parameter ADD maintenance_message character varying;
ALTER TABLE sn_parameter ADD release_version character varying;





#
#Å@éËìñä«óù
#

CREATE TABLE ms_si_allowance
(
    ms_si_allowance_id numeric(9,0) NOT NULL PRIMARY KEY,
    name character varying(50) NOT NULL,
    contents character varying(500),
    target_vessel character varying NOT NULL,
    department numeric(1,0) NOT NULL DEFAULT 9,
    allowance numeric(6,0) NOT NULL DEFAULT 0,
    ms_si_shokumei_id numeric(9,0),

    delete_flag numeric(1,0) NOT NULL DEFAULT 0,
    send_flag numeric(1,0) NOT NULL DEFAULT 0,
    vessel_id numeric(4,0) NOT NULL,
    user_key character varying(40),
    data_no numeric(13,0),
    renew_date timestamp without time zone NOT NULL,
    renew_user_id character varying(40) NOT NULL,
    ts character varying(20)
);

CREATE TABLE si_allowance
(
    si_allowance_id character varying(50) NOT NULL PRIMARY KEY,
    ms_si_allowance_id numeric(9,0) NOT NULL,
    ms_vessel_id numeric(4,0) NOT NULL,
    captain_senin_id numeric(9,0) NOT NULL,
    year_month character varying(6) NOT NULL,
    contents character varying(500),
    quantity numeric(3,0) NOT NULL DEFAULT 0,
    allowance numeric(6,0) NOT NULL DEFAULT 0,
    person_in_charge character varying(500),

    delete_flag numeric(1,0) NOT NULL DEFAULT 0,
    send_flag numeric(1,0) NOT NULL DEFAULT 0,
    vessel_id numeric(4,0) NOT NULL,
    user_key character varying(40),
    data_no numeric(13,0),
    renew_date timestamp without time zone NOT NULL,
    renew_user_id character varying(40) NOT NULL,
    ts character varying(20)
);

CREATE TABLE si_allowance_detail
(
    si_allowance_detail_id character varying(50) NOT NULL PRIMARY KEY,    
    si_allowance_id character varying(50) NOT NULL,
    ms_senin_id numeric(9,0) NOT NULL,
    ms_si_shokumei_id numeric(9,0) NOT NULL,
    allowance numeric(6,0) NOT NULL DEFAULT 0,
    is_target numeric(1,0) NOT NULL DEFAULT 0,

    delete_flag numeric(1,0) NOT NULL DEFAULT 0,
    send_flag numeric(1,0) NOT NULL DEFAULT 0,
    vessel_id numeric(4,0) NOT NULL,
    user_key character varying(40) ,
    data_no numeric(13,0),
    renew_date timestamp without time zone NOT NULL,
    renew_user_id character varying(40) NOT NULL,
    ts character varying(20)
);




INSERT INTO SN_TABLE_INFO (NAME)
SELECT 'si_allowance' WHERE NOT EXISTS ( SELECT name FROM SN_TABLE_INFO WHERE NAME = 'si_allowance');

INSERT INTO SN_TABLE_INFO (NAME)
SELECT 'si_allowance_detail' WHERE NOT EXISTS ( SELECT name FROM SN_TABLE_INFO WHERE NAME = 'si_allowance_detail');




ALTER TABLE ms_si_allowance ADD show_order numeric(9,0) default 0;

ALTER TABLE si_allowance ALTER COLUMN quantity TYPE numeric(6, 0);

ALTER TABLE ms_si_allowance ADD distribution_flag numeric(1, 0) default 0;

ALTER TABLE si_allowance_detail ADD si_card_id character varying(40); 


