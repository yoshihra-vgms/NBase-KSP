
CREATE TABLE sync_tables_#TABLE_KEY#
(
    CONSTRAINT pk_sync_tables_#TABLE_KEY# PRIMARY KEY (site_id, type_id, table_no)
)  INHERITS (sync_tables);




CREATE TABLE setting_#TABLE_KEY#
(
    CONSTRAINT pk_setting_#TABLE_KEY# PRIMARY KEY (site_id)
)  INHERITS (setting);



CREATE TABLE work_content_#TABLE_KEY#
(
    CONSTRAINT pk_work_content_#TABLE_KEY# PRIMARY KEY (site_id, work_content_id)
)  INHERITS (work_content);


CREATE TABLE role_#TABLE_KEY#
(
    CONSTRAINT pk_role_#TABLE_KEY# PRIMARY KEY (site_id, role_id)
)  INHERITS (role);

CREATE TABLE rank_category_#TABLE_KEY#
(
    CONSTRAINT pk_rank_category_#TABLE_KEY# PRIMARY KEY (site_id, rank_category_id)
)  INHERITS (rank_category);





CREATE TABLE work_#TABLE_KEY#
(
    CONSTRAINT pk_work_#TABLE_KEY# PRIMARY KEY (site_id, work_id)
)  INHERITS (work);


CREATE TABLE summary_times_#TABLE_KEY#
(
    CONSTRAINT pk_summary_times_#TABLE_KEY# PRIMARY KEY (site_id, summary_times_id)
)  INHERITS (summary_times);


CREATE TABLE vessel_movement_#TABLE_KEY#
(
    CONSTRAINT pk_vessel_movement_#TABLE_KEY# PRIMARY KEY (site_id, vessel_id, date_info)
)  INHERITS (vessel_movement);



CREATE TABLE vessel_approval_day_#TABLE_KEY#
(
    CONSTRAINT pk_vessel_approval_day_#TABLE_KEY# PRIMARY KEY (site_id, vessel_approval_day_id)
)  INHERITS (vessel_approval_day);

CREATE TABLE vessel_approval_month_#TABLE_KEY#
(
    CONSTRAINT pk_vessel_approval_month_#TABLE_KEY# PRIMARY KEY (site_id, vessel_approval_month_id)
)  INHERITS (vessel_approval_month);





INSERT INTO sync_tables_#TABLE_KEY#(
	site_id, type_id, table_no, table_name, alive)
	VALUES ('#SITE_ID#', 0, 1, 'setting', true);

INSERT INTO sync_tables_#TABLE_KEY#(
	site_id, type_id, table_no, table_name, alive)
	VALUES ('#SITE_ID#', 0, 2, 'work_content', true);


INSERT INTO sync_tables_#TABLE_KEY#(
	site_id, type_id, table_no, table_name, alive)
	VALUES ('#SITE_ID#', 0, 3, 'role', true);


INSERT INTO sync_tables_#TABLE_KEY#(
	site_id, type_id, table_no, table_name, alive)
	VALUES ('#SITE_ID#', 0, 4, 'rank_category', true);




INSERT INTO sync_tables_#TABLE_KEY#(
	site_id, type_id, table_no, table_name, alive)
	VALUES ('#SITE_ID#', 1, 1, 'work', true);


INSERT INTO sync_tables_#TABLE_KEY#(
	site_id, type_id, table_no, table_name, alive)
	VALUES ('#SITE_ID#', 1, 2, 'summary_times', true);

INSERT INTO sync_tables_#TABLE_KEY#(
	site_id, type_id, table_no, table_name, alive)
	VALUES ('#SITE_ID#', 1, 3, 'vessel_movement', true);

INSERT INTO sync_tables_#TABLE_KEY#(
	site_id, type_id, table_no, table_name, alive)
	VALUES ('#SITE_ID#', 1, 4, 'vessel_approval_day', true);

INSERT INTO sync_tables_#TABLE_KEY#(
	site_id, type_id, table_no, table_name, alive)
	VALUES ('#SITE_ID#', 1, 5, 'vessel_approval_month', true);









