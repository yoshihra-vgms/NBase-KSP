-- Table: ms_viq_version

-- DROP TABLE ms_viq_version;

CREATE TABLE ms_viq_version
(
  viq_version_id serial NOT NULL,
  viq_version character varying,
  start_date timestamp without time zone,
  end_date timestamp without time zone,



  delete_flag boolean DEFAULT false,
  create_ms_user_id character varying(40),
  update_ms_user_id character varying(40),
  create_date timestamp without time zone,
  update_date timestamp without time zone,
  CONSTRAINT pk_ms_viq_version PRIMARY KEY (viq_version_id)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE ms_viq_version
  OWNER TO "IGTDefconUser";
COMMENT ON TABLE ms_viq_version
  IS 'VIQ Versionマスタ';

-- Trigger: tr_ms_viq_version on ms_viq_version

-- DROP TRIGGER tr_ms_viq_version ON ms_viq_version;

CREATE TRIGGER tr_ms_viq_version
  BEFORE INSERT OR UPDATE
  ON ms_viq_version
  FOR EACH ROW
  EXECUTE PROCEDURE "TrAutoDateUpdate"();