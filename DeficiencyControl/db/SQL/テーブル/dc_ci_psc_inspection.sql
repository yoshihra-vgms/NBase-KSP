-- Table: dc_ci_psc_inspection

-- DROP TABLE dc_ci_psc_inspection;

CREATE TABLE dc_ci_psc_inspection
(

  deficinecy_count int,
  
  ms_basho_id character varying(40),
  ms_regional_code character varying(10),
  
  comment_remarks text,
  share_to_our_fleet boolean,
  share_to_our_fleet_date timestamp without time zone,
  
  deficinecy_no int,
  status_id int,
  
  ms_user_id character varying(40),
  action_taken_by_vessel text,
  class_involved_nk_department character varying,
  class_involved_nk_name character varying,
  class_involved text,
  
  deficiency_code_id int,
  deficiency text,
  cause_of_deficiency text,
  action_taken_by_company text,
  corrective_action text,
  item_remarks text,

  
  CONSTRAINT pk_dc_ci_psc_inspection PRIMARY KEY (comment_item_id )
)
INHERITS (dc_comment_item)
WITH (
  OIDS=FALSE
);
ALTER TABLE dc_ci_psc_inspection
  OWNER TO "IGTDefconUser";
COMMENT ON TABLE dc_ci_psc_inspection
  IS 'PSC Inspectionアイテムテーブル';

-- Trigger: tr_dc_ci_psc_inspection on dc_ci_psc_inspection

-- DROP TRIGGER tr_dc_ci_psc_inspection ON dc_ci_psc_inspection;

CREATE TRIGGER tr_dc_ci_psc_inspection
  BEFORE INSERT OR UPDATE
  ON dc_ci_psc_inspection
  FOR EACH ROW
  EXECUTE PROCEDURE "TrAutoDateUpdate"();

