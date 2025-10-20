-- Table: ms_accident_importance

-- DROP TABLE ms_accident_importance;

CREATE TABLE ms_accident_importance
(
  accident_importance_id serial NOT NULL,
  accident_importance_name character varying,

  delete_flag boolean DEFAULT false,
  create_ms_user_id character varying(40),
  update_ms_user_id character varying(40),
  create_date timestamp without time zone,
  update_date timestamp without time zone,
  CONSTRAINT pk_ms_accident_importance PRIMARY KEY (accident_importance_id )
)
WITH (
  OIDS=FALSE
);
ALTER TABLE ms_accident_importance
  OWNER TO "IGTDefconUser";
COMMENT ON TABLE ms_accident_importance
  IS 'Accident Importanceマスタ';

-- Trigger: tr_ms_accident_importance on ms_accident_importance

-- DROP TRIGGER tr_ms_accident_importance ON ms_accident_importance;

CREATE TRIGGER tr_ms_accident_importance
  BEFORE INSERT OR UPDATE
  ON ms_accident_importance
  FOR EACH ROW
  EXECUTE PROCEDURE "TrAutoDateUpdate"();