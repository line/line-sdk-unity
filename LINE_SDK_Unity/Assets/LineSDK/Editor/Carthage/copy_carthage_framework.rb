#!/usr/bin/env ruby

require 'rubygems'
require 'bundler/setup'

require 'xcodeproj'

proj = Xcodeproj::Project.new('Unity-iPhone.xcodeproj')
proj.initialize_from_file

proj.targets.each do |target|
  if target.name == 'Unity-iPhone'
    phase_name = 'Carthage Copy Frameworks (LINE SDK)'
    # Delete if exists
    target.build_phases.delete_if do |phase|
        phase.is_a?(Xcodeproj::Project::Object::PBXShellScriptBuildPhase) && phase.name == phase_name
    end
    # Create a new one
    run_script = proj.new(Xcodeproj::Project::Object::PBXShellScriptBuildPhase)
    run_script.name = phase_name
    run_script.shell_script = '/usr/local/bin/carthage copy-frameworks'

    frameworks = ['LineSDK.framework', 'LineSDKObjC.framework']
    run_script.input_paths = frameworks.map do |framework|
        "$(SRCROOT)/Carthage/Build/iOS/#{framework}"
    end
    run_script.output_paths = frameworks.map do |framework|
        "$(BUILT_PRODUCTS_DIR)/$(FRAMEWORKS_FOLDER_PATH)/#{framework}"
    end

    target.build_phases << run_script
  end
end

proj.save
