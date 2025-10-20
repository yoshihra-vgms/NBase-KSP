-- Table: ms_accident_kind

-- DROP TABLE ms_accident_kind;

CREATE TABLE ms_accident_kind
(
  accident_kind_id serial NOT NULL,
  accident_kind_name character varying,

  delete_flag boolean DEFAULT false,
  create_ms_user_id character varying(40),
  update_ms_user_id character varying(40),
  create_date timestamp without time zone,
  update_date timestamp without time zone,
  CONSTRAINT pk_ms_accident_kind PRIMARY KEY (accident_kind_id )
)
WITH (
  OIDS=FALSE
);
ALTER TABLE ms_accident_kind
  OWNER TO "IGTDefconUser";
COMMENT ON TABLE ms_accident_kind
  IS 'Accident種類マスタ';

-- Trigger: tr_ms_accident_kind on ms_accident_kind

-- DROP TRIGGER tr_ms_accident_kind ON ms_accident_kind;

CREATE TRIGGER tr_ms_accident_kind
  BEFORE INSERT OR UPDATE
  ON ms_accident_kind
  FOR EACH ROW
  EXECUTE PROCEDURE "TrAutoDateUpdate"();