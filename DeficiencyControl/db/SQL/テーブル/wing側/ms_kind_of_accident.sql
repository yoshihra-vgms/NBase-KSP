-- Table: ms_kind_of_accident

-- DROP TABLE ms_kind_of_accident;

CREATE TABLE ms_kind_of_accident
(
  kind_of_accident_id serial NOT NULL,
  kind_of_accident_name character varying,

  delete_flag boolean DEFAULT false,
  create_ms_user_id character varying(40),
  update_ms_user_id character varying(40),
  create_date timestamp without time zone,
  update_date timestamp without time zone,
  CONSTRAINT pk_ms_kind_of_accident PRIMARY KEY (kind_of_accident_id )
)
WITH (
  OIDS=FALSE
);
ALTER TABLE ms_kind_of_accident
  OWNER TO "IGTDefconUser";
COMMENT ON TABLE ms_kind_of_accident
  IS 'Kind of Accidentマスタ';

-- Trigger: tr_ms_kind_of_accident on ms_kind_of_accident

-- DROP TRIGGER tr_ms_kind_of_accident ON ms_kind_of_accident;

CREATE TRIGGER tr_ms_kind_of_accident
  BEFORE INSERT OR UPDATE
  ON ms_kind_of_accident
  FOR EACH ROW
  EXECUTE PROCEDURE "TrAutoDateUpdate"();