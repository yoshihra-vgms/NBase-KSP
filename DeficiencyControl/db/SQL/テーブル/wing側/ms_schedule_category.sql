-- Table: ms_schedule_category

-- DROP TABLE ms_schedule_category;

CREATE TABLE ms_schedule_category
(
  schedule_category_id serial NOT NULL,
  schedule_category_name character varying,

  delete_flag boolean DEFAULT false,
  create_ms_user_id character varying(40),
  update_ms_user_id character varying(40),
  create_date timestamp without time zone,
  update_date timestamp without time zone,
  CONSTRAINT pk_ms_schedule_category PRIMARY KEY (schedule_category_id )
)
WITH (
  OIDS=FALSE
);
ALTER TABLE ms_schedule_category
  OWNER TO "IGTDefconUser";
COMMENT ON TABLE ms_schedule_category
  IS 'スケジュール区分マスタ';

-- Trigger: tr_ms_schedule_category on ms_schedule_category

-- DROP TRIGGER tr_ms_schedule_category ON ms_schedule_category;

CREATE TRIGGER tr_ms_schedule_category
  BEFORE INSERT OR UPDATE
  ON ms_schedule_category
  FOR EACH ROW
  EXECUTE PROCEDURE "TrAutoDateUpdate"();