//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 3.0.12
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class vx_state_account_t : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal vx_state_account_t(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(vx_state_account_t obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~vx_state_account_t() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          VivoxCoreInstancePINVOKE.delete_vx_state_account_t(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
    }
  }

  public string account_handle {
    set {
      VivoxCoreInstancePINVOKE.vx_state_account_t_account_handle_set(swigCPtr, value);
    } 
    get {
      string ret = VivoxCoreInstancePINVOKE.vx_state_account_t_account_handle_get(swigCPtr);
      return ret;
    } 
  }

  public string account_uri {
    set {
      VivoxCoreInstancePINVOKE.vx_state_account_t_account_uri_set(swigCPtr, value);
    } 
    get {
      string ret = VivoxCoreInstancePINVOKE.vx_state_account_t_account_uri_get(swigCPtr);
      return ret;
    } 
  }

  public string display_name {
    set {
      VivoxCoreInstancePINVOKE.vx_state_account_t_display_name_set(swigCPtr, value);
    } 
    get {
      string ret = VivoxCoreInstancePINVOKE.vx_state_account_t_display_name_get(swigCPtr);
      return ret;
    } 
  }

  public int is_anonymous_login {
    set {
      VivoxCoreInstancePINVOKE.vx_state_account_t_is_anonymous_login_set(swigCPtr, value);
    } 
    get {
      int ret = VivoxCoreInstancePINVOKE.vx_state_account_t_is_anonymous_login_get(swigCPtr);
      return ret;
    } 
  }

  public int state_sessiongroups_count {
    set {
      VivoxCoreInstancePINVOKE.vx_state_account_t_state_sessiongroups_count_set(swigCPtr, value);
    } 
    get {
      int ret = VivoxCoreInstancePINVOKE.vx_state_account_t_state_sessiongroups_count_get(swigCPtr);
      return ret;
    } 
  }

  public vx_login_state_change_state state {
    set {
      VivoxCoreInstancePINVOKE.vx_state_account_t_state_set(swigCPtr, (int)value);
    } 
    get {
      vx_login_state_change_state ret = (vx_login_state_change_state)VivoxCoreInstancePINVOKE.vx_state_account_t_state_get(swigCPtr);
      return ret;
    } 
  }

  public SWIGTYPE_p_p_vx_state_sessiongroup state_sessiongroups {
    set {
      VivoxCoreInstancePINVOKE.vx_state_account_t_state_sessiongroups_set(swigCPtr, SWIGTYPE_p_p_vx_state_sessiongroup.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = VivoxCoreInstancePINVOKE.vx_state_account_t_state_sessiongroups_get(swigCPtr);
      SWIGTYPE_p_p_vx_state_sessiongroup ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_p_vx_state_sessiongroup(cPtr, false);
      return ret;
    } 
  }

  public int state_buddy_count {
    set {
      VivoxCoreInstancePINVOKE.vx_state_account_t_state_buddy_count_set(swigCPtr, value);
    } 
    get {
      int ret = VivoxCoreInstancePINVOKE.vx_state_account_t_state_buddy_count_get(swigCPtr);
      return ret;
    } 
  }

  public int state_buddy_group_count {
    set {
      VivoxCoreInstancePINVOKE.vx_state_account_t_state_buddy_group_count_set(swigCPtr, value);
    } 
    get {
      int ret = VivoxCoreInstancePINVOKE.vx_state_account_t_state_buddy_group_count_get(swigCPtr);
      return ret;
    } 
  }

  public SWIGTYPE_p_p_vx_state_buddy state_buddies {
    set {
      VivoxCoreInstancePINVOKE.vx_state_account_t_state_buddies_set(swigCPtr, SWIGTYPE_p_p_vx_state_buddy.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = VivoxCoreInstancePINVOKE.vx_state_account_t_state_buddies_get(swigCPtr);
      SWIGTYPE_p_p_vx_state_buddy ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_p_vx_state_buddy(cPtr, false);
      return ret;
    } 
  }

  public SWIGTYPE_p_p_vx_state_buddy_group state_buddy_groups {
    set {
      VivoxCoreInstancePINVOKE.vx_state_account_t_state_buddy_groups_set(swigCPtr, SWIGTYPE_p_p_vx_state_buddy_group.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = VivoxCoreInstancePINVOKE.vx_state_account_t_state_buddy_groups_get(swigCPtr);
      SWIGTYPE_p_p_vx_state_buddy_group ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_p_vx_state_buddy_group(cPtr, false);
      return ret;
    } 
  }

  public vx_state_account_t() : this(VivoxCoreInstancePINVOKE.new_vx_state_account_t(), true) {
  }

}