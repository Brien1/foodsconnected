#!/bin/bash
rm -r TestResults
mkdir TestResults
dotnet test --collect:"XPlat Code Coverage"
dotnet reportgenerator -reports:`find TestResults -name coverage.cobertura.xml` -targetdir:coverage_rendered  
