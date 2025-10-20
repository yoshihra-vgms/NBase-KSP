-- Table: ms_accident_status

-- DROP TABLE ms_accident_status;

CREATE TABLE ms_accident_status
(
  accident_status_id serial NOT NULL,
  accident_status_name character varying,

  delete_flag boolean DEFAULT false,
  create_ms_user_id character varying(40),
  update_ms_user_id character varying(40),
  create_date timestamp without time zone,
  update_date timestamp without time zone,
  CONSTRAINT pk_ms_accident_status PRIMARY KEY (accident_status_id )
)
WITH (
  OIDS=FALSE
);
ALTER TABLE ms_accident_status
  OWNER TO "IGTDefconUser";
COMMENT ON TABLE ms_accident_status
  IS 'Accident状態マスタ';

-- Trigger: tr_ms_accident_status on ms_accident_status

-- DROP TRIGGER tr_ms_accident_status ON ms_accident_status;

CREATE TRIGGER tr_ms_accident_status
  BEFORE INSERT OR UPDATE
  ON ms_accident_status
  FOR EACH ROW
  EXECUTE PROCEDURE "TrAutoDateUpdate"();