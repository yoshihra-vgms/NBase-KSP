-- Table: dc_accident_progress

-- DROP TABLE dc_accident_progress;

CREATE TABLE dc_accident_progress
(
  accident_progress_id serial NOT NULL,
  
  accident_id integer,
  date timestamp without time zone,
  progress text,
  
  delete_flag boolean DEFAULT false,
  create_ms_user_id character varying(40),
  update_ms_user_id character varying(40),
  create_date timestamp without time zone,
  update_date timestamp without time zone,
  CONSTRAINT pk_dc_accident_progress PRIMARY KEY (accident_progress_id )
)
WITH (
  OIDS=FALSE
);
ALTER TABLE dc_accident_progress
  OWNER TO "IGTDefconUser";
COMMENT ON TABLE dc_accident_progress
  IS '事故トラブル進捗テーブル';

-- Trigger: tr_dc_accident_progress on dc_accident_progress

-- DROP TRIGGER tr_dc_accident_progress ON dc_accident_progress;

CREATE TRIGGER tr_dc_accident_progress
  BEFORE INSERT OR UPDATE
  ON dc_accident_progress
  FOR EACH ROW
  EXECUTE PROCEDURE "TrAutoDateUpdate"();

