function [ probes ] = generate_probes( training_data, backward_data_usage_depth )

probes = [];
for i = backward_data_usage_depth + 1:size(training_data, 2)
    probe = [];
    for j = 0 : backward_data_usage_depth
       probe = [probe; training_data(:, i - j)];
    end    
    probes = [probes, probe];
end

end

