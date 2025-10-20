-- Table: ms_accident_situation

-- DROP TABLE ms_accident_situation;

CREATE TABLE ms_accident_situation
(
  accident_situation_id serial NOT NULL,
  accident_situation_name character varying,

  delete_flag boolean DEFAULT false,
  create_ms_user_id character varying(40),
  update_ms_user_id character varying(40),
  create_date timestamp without time zone,
  update_date timestamp without time zone,
  CONSTRAINT pk_ms_accident_situation PRIMARY KEY (accident_situation_id )
)
WITH (
  OIDS=FALSE
);
ALTER TABLE ms_accident_situation
  OWNER TO "IGTDefconUser";
COMMENT ON TABLE ms_accident_situation
  IS 'Accident発生状況マスタ';

-- Trigger: tr_ms_accident_situation on ms_accident_situation

-- DROP TRIGGER tr_ms_accident_situation ON ms_accident_situation;

CREATE TRIGGER tr_ms_accident_situation
  BEFORE INSERT OR UPDATE
  ON ms_accident_situation
  FOR EACH ROW
  EXECUTE PROCEDURE "TrAutoDateUpdate"();