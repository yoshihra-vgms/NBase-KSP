-- Table: ms_schedule_kind

-- DROP TABLE ms_schedule_kind;

CREATE TABLE ms_schedule_kind
(
  schedule_kind_id serial NOT NULL,
  schedule_category_id integer,
  schedule_kind_name character varying,

  delete_flag boolean DEFAULT false,
  create_ms_user_id character varying(40),
  update_ms_user_id character varying(40),
  create_date timestamp without time zone,
  update_date timestamp without time zone,
  CONSTRAINT pk_ms_schedule_kind PRIMARY KEY (schedule_kind_id )
)
WITH (
  OIDS=FALSE
);
ALTER TABLE ms_schedule_kind
  OWNER TO "IGTDefconUser";
COMMENT ON TABLE ms_schedule_kind
  IS 'スケジュール種別マスタ';

-- Trigger: tr_ms_schedule_kind on ms_schedule_kind

-- DROP TRIGGER tr_ms_schedule_kind ON ms_schedule_kind;

CREATE TRIGGER tr_ms_schedule_kind
  BEFORE INSERT OR UPDATE
  ON ms_schedule_kind
  FOR EACH ROW
  EXECUTE PROCEDURE "TrAutoDateUpdate"();