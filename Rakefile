require 'colorize'

desc "Release A New Version"
task :release do
    puts "Releasing Version: ".bold
    version = STDIN.gets.strip

    puts "Exporting unity package...".bold
    unity_app = "/Applications/Unity/Hub/Editor/2019.4.28f1/Unity.app"
    working_path = `pwd`.strip
    proj_path = "#{working_path}/LINE_SDK_Unity"
    puts `#{unity_app}/Contents/MacOS/Unity -batchmode -projectPath #{proj_path} -exportPackage Assets/Plugins Assets/LineSDK output.unitypackage -quit`
    FileUtils.mkdir "Release" unless File.exists?("Release")
    FileUtils.mv "#{proj_path}/output.unitypackage", "./Release/line_sdk_unity_#{version}.unitypackage"

    Rake::Task["updateDocVersion"].invoke(version)

    `open ./Release`
end

desc "Update version in Doxyfile"
task :updateDocVersion, [:version] do |task, args|
    puts "work", args[:version]
    text = File.read('Doxyfile')
    new_contents = text.gsub(/PROJECT_NUMBER\s+=\s+(\d+\.)?(\d+\.)?(\*|\d+)/, "PROJECT_NUMBER         = #{args[:version]}")
    puts new_contents

    File.open("Doxyfile", "w") {|file| file.puts new_contents }
end